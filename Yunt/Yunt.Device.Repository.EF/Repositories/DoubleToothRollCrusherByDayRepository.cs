using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Yunt.Device.Domain.IRepository;
using Yunt.Device.Domain.Model;
using Yunt.Common;
using Yunt.Redis;
using AutoMapper.XpressionMapper.Extensions;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Yunt.Device.Repository.EF.Repositories
{
    public class DoubleToothRollCrusherByDayRepository : DeviceRepositoryBase<DoubleToothRollCrusherByDay, Models.DoubleToothRollCrusherByDay>, IDoubleToothRollCrusherByDayRepository
    {

        private readonly IDoubleToothRollCrusherByHourRepository _dtrRep;
        private readonly IMotorRepository _motorRep;

        public DoubleToothRollCrusherByDayRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
            _dtrRep = ServiceProviderServiceExtensions.GetService<IDoubleToothRollCrusherByHourRepository>(BootStrap.ServiceProvider);
            _motorRep = ServiceProviderServiceExtensions.GetService<IMotorRepository>(BootStrap.ServiceProvider);
        }
        #region extend method
      
        #endregion

    }
}
