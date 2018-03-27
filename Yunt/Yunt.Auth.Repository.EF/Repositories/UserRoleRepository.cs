using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Yunt.Auth.Domain.IRepository;
using Yunt.Auth.Domain.Model;
using Yunt.Auth.Domain.Model.IdModel;
using Yunt.Common;
using Yunt.Redis;

namespace Yunt.Auth.Repository.EF.Repositories
{
    public class UserRoleRepository : AuthRepositoryBase<UserRole, Models.UserRole>, IUserRoleRepository
    {
     
        public UserRoleRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
          
        }


}
}
