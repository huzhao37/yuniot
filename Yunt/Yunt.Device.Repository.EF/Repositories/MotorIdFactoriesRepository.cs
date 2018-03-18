using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Yunt.Common;
using Yunt.Device.Domain.IRepository;
using Yunt.Device.Domain.Model;
using Yunt.Device.Domain.Model.IdModel;
using Yunt.Redis;

namespace Yunt.Device.Repository.EF.Repositories
{
    public class MotorIdFactoriesRepository : DeviceRepositoryBase<MotorIdFactories, Models.IdModel.MotorIdFactories>, IMotorIdFactoriesRepository
    {
     
        public MotorIdFactoriesRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
          
        }


}
}
