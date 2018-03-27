﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Yunt.Device.Domain.Model;

namespace Yunt.Device.Domain.IRepository
{
    public interface IVerticalCrusherByHourRepository : IDeviceRepositoryBase<VerticalCrusherByHour>
    {
        #region extend method

        /// <summary>
        /// 统计该小时的立轴数据;
        /// </summary>
        /// <param name="motorId">设备id</param>
        /// <param name="isExceed">是否超过一天的数据范围</param>
        /// <param name="dt">查询时间,精确到小时</param>
        /// <returns></returns>
        VerticalCrusherByHour GetByMotorId(string motorId, bool isExceed, DateTime dt);

        /// <summary>
        /// 统计该小时内所有立轴的数据;
        /// </summary>
        /// <param name="dt">时间</param>
        /// <param name="MotorTypeId">设备类型</param>
        Task InsertHourStatistics(DateTime dt, string MotorTypeId);

        #endregion

    }
}
