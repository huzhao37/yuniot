using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Yunt.Analysis.Domain.IRepository;
using Yunt.Analysis.Domain.Model;
using Yunt.Common;
using Yunt.Redis;

namespace Yunt.Analysis.Repository.EF.Repositories
{
    public class MotorEventLogRepository : AnalysisRepositoryBase<MotorEventLog, Models.MotorEventLog>, IMotorEventLogRepository
    {
     
        public MotorEventLogRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
          
        }
        /// <summary>
        /// 获取该redis数据库中所有key键
        /// </summary>
        /// <returns></returns>
        public List<string> GetRedisAllKeys()
        {
            RedisProvider.DB = 1;
            return RedisProvider.Keys("*");
        }
        /// <summary>
        /// 添加分析元数据
        /// </summary>
        /// <param name="log"></param>
        public async Task AddAiLogAsync(AiLog log)
        {
            long dayUnix = log.Time.Time().Date.TimeSpan();
            RedisProvider.DB = 1;
            RedisProvider.LPUSH(dayUnix+"|"+log.MotorId, log, DataType.Protobuf);   //若要提高解析数据速度，可更改为异步LpushAsync //备用 
            RedisProvider.Expire(dayUnix + "|" + log.MotorId, dayUnix.ExpireOneDay());            
        }
        /// <summary>
        /// 获取分析元数据
        /// </summary>
        /// <param name="motorId"></param>
        /// <returns></returns>
        public IEnumerable<AiLog> GetAiLogs(string motorId)
        {
            long dayUnix = DateTime.Now.Date.AddDays(-1).TimeSpan();
            RedisProvider.DB = 1;
            return RedisProvider.ListRange<AiLog>(dayUnix + "|" + motorId, DataType.Protobuf);
          
        }
        /// <summary>
        /// 添加数字量历史记录
        /// </summary>
        /// <param name="di"></param>
        /// <returns></returns>
        public async Task AddDiLogAsync(DiHistory di)
        {
            long dayUnix = di.Time.Time().Date.TimeSpan();
            RedisProvider.DB = 3;
            RedisProvider.LPUSH(dayUnix+"|"+ di.MotorId, di, DataType.Protobuf);//若要提高解析数据速度，可更改为异步LpushAsync  //备用  
            RedisProvider.Expire(dayUnix + "|" + di.MotorId, dayUnix.Expire());
        }
        /// <summary>
        /// 获取设备数字量记录
        /// </summary>
        /// <param name="motorId"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public IEnumerable<DiHistory> GetDis(string motorId, DateTime dt)
        {
            long dayUnix = dt.Date.TimeSpan();
            RedisProvider.DB = 3;
            return RedisProvider.ListRange<DiHistory>(dayUnix + "|" + motorId, DataType.Protobuf);
        }
    }
}
