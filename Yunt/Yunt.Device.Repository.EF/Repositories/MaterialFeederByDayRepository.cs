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

namespace Yunt.Device.Repository.EF.Repositories
{
    public class MaterialFeederByDayRepository : DeviceRepositoryBase<MaterialFeederByDay, Models.MaterialFeederByDay>, IMaterialFeederByDayRepository
    {

        public MaterialFeederByDayRepository( IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
            
        }
        
    }
}
