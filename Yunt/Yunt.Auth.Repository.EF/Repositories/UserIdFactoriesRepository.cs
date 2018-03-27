using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Yunt.Auth.Domain.IRepository;
using Yunt.Auth.Domain.Model.IdModel;
using Yunt.Common;
using Yunt.Redis;

namespace Yunt.Auth.Repository.EF.Repositories
{
    public class UserIdFactoriesRepository : AuthRepositoryBase<UserIdFactories, Models.IdModel.UserIdFactories>, IUserIdFactoriesRepository
    {
     
        public UserIdFactoriesRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
          
        }


}
}
