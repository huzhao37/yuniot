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
    public class DataconfigRepository : XmlRepositoryBase<Dataconfig, Models.Dataconfig>, IDataconfigRepository
    {
     
        public DataconfigRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
          
        }


}
}
