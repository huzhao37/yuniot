using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Yunt.Common;
using Yunt.Xml.Domain.IRepository;
using Yunt.Xml.Domain.Model;

using Yunt.Redis;

namespace Yunt.Xml.Repository.EF.Repositories
{
    public class MotorparamsRepository : XmlRepositoryBase<Motorparams, Models.Motorparams>, IMotorparamsRepository
    {
     
        public MotorparamsRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
          
        }


}
}
