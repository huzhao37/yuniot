using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using AutoMapper.XpressionMapper.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Yunt.Analysis.Domain.IRepository;
using Yunt.Analysis.Domain.Model;
using Yunt.Common;
using Yunt.Redis;

namespace Yunt.Analysis.Repository.EF.Repositories
{
    public class EventKindRepository : AnalysisRepositoryBase<EventKind, Models.EventKind>, IEventKindRepository
    {

        public EventKindRepository(IMapper mapper, IRedisCachingProvider provider) : base(mapper, provider)
        {           
        }


    }
}
