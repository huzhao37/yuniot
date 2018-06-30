using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yunt.Analysis.Domain.Model;
using Yunt.Common;

namespace Yunt.Analysis.Domain.IRepository
{
    public interface IMotorEventLogRepository : IAnalysisRepositoryBase<MotorEventLog>
    {
        /// <summary>
        /// 获取该redis数据库中所有key键
        /// </summary>
        /// <returns></returns>
        List<string> GetRedisAllKeys();
        /// <summary>
        /// 添加分析元数据
        /// </summary>
        /// <param name="log"></param>
        Task AddAiLogAsync(AiLog log);

        /// <summary>
        /// 获取分析元数据
        /// </summary>
        /// <param name="motorId"></param>
        /// <returns></returns>
        IEnumerable<AiLog> GetAiLogs(string motorId);
        /// <summary>
        /// 获取分析元数据
        /// </summary>
        /// <param name="motorId"></param>
        /// <returns></returns>
         IEnumerable<AiLog> GetAiLogsByKey(string key);
        /// <summary>
        /// 添加数字量元数据
        /// </summary>
        /// <param name="di"></param>
        Task AddDiLogAsync(DiHistory di);

        /// <summary>
        /// 获取数字量元数据
        /// </summary>
        /// <param name="motorId">设备编码</param>
        /// <param name="dt">某一天</param>
        /// <returns></returns>
        IEnumerable<DiHistory> GetDis(string motorId,DateTime dt);
    }
}
