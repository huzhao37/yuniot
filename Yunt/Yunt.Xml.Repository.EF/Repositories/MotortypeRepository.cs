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
    public class MotortypeRepository : XmlRepositoryBase<Motortype, Models.Motortype>, IMotortypeRepository
    {
     
        public MotortypeRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
          
        }


}
}
