using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Yunt.Common;
using Yunt.Xml.Domain.IRepository;

using Yunt.Redis;
using Yunt.Xml.Domain.Model;

namespace Yunt.Xml.Repository.EF.Repositories
{
    public class CollectdeviceRepository : XmlRepositoryBase<Collectdevice, Models.Collectdevice>, ICollectdeviceRepository
    {
     
        public CollectdeviceRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
          
        }


}
}
