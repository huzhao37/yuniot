﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Yunt.Common;
using Yunt.Xml.Domain.IRepository;
using Yunt.Xml.Domain.Model;

using Yunt.Redis;

namespace Yunt.Xml.Repository.EF.Repositories
{
    public class ProductionPlansRepository : XmlRepositoryBase<ProductionPlans, Models.ProductionPlans>, IProductionPlansRepository
    {
     
        public ProductionPlansRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {
          
        }

    }
}
