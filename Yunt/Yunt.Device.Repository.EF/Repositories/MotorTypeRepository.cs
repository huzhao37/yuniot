using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using AutoMapper.XpressionMapper.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Yunt.Common;
using Yunt.Device.Domain.IRepository;
using Yunt.Device.Domain.Model;
using Yunt.Device.Domain.Model.IdModel;
using Yunt.Redis;

namespace Yunt.Device.Repository.EF.Repositories
{
    public class MotorTypeRepository : DeviceRepositoryBase<MotorType, Models.MotorType>, IMotorTypeRepository
    {

        public MotorTypeRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {           
        }

      

    }
}
