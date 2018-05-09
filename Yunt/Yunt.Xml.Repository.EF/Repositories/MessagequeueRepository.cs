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
    public class MessagequeueRepository : XmlRepositoryBase<Messagequeue, Models.Messagequeue>, IMessagequeueRepository
    {
     
        public MessagequeueRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
          
        }


}
}
