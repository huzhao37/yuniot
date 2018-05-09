using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Yunt.Device.Repository.EF.Repositories
{
    public class OriginalBytesRepository : DeviceRepositoryBase<OriginalBytes, Models.OriginalBytes>, IOriginalBytesRepository
    {
        public OriginalBytesRepository( IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
            
        }

       
    }
}
