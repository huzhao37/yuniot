using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Yunt.Analysis.Domain.IRepository;
using Yunt.Analysis.Domain.Model;
using Yunt.Common;
using Yunt.Redis;

namespace Yunt.Analysis.Repository.EF.Repositories
{
    public class MaintainRepository : AnalysisRepositoryBase<Maintain, Models.Maintain>, IMaintainRepository
    {
     
        public MaintainRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
          
        }
  
    }
}
