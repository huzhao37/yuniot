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

namespace Yunt.Device.Repository.EF.Repositories
{
    public class ConveyorRepository : DeviceRepositoryBase<Conveyor, Models.Conveyor>, IConveyorRepository
    {

        public ConveyorRepository( IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
            
        }

     

        
    }
}
