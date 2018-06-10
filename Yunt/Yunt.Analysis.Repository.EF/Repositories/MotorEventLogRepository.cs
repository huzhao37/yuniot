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
    public class EventKindRoleRepository : AnalysisRepositoryBase<MotorEventLog, Models.MotorEventLog>, IMotorEventLogRepository
    {
     
        public EventKindRoleRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
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
            await RedisProvider.LpushAsync(log.MotorId, log, DataType.Protobuf);         
            RedisProvider.Expire(log.MotorId, dayUnix.ExpireOneDay());            
        }
        /// <summary>
        /// 获取分析元数据
        /// </summary>
        /// <param name="motorId"></param>
        /// <returns></returns>
        public IEnumerable<AiLog> GetAiLogs(string motorId)
        {
            RedisProvider.DB = 1;
            return RedisProvider.ListRange<AiLog>(motorId, DataType.Protobuf);
          
        }

    }
}
