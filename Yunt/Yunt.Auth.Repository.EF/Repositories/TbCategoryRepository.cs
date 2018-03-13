using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Yunt.Auth.Domain.IRepository;
using Yunt.Auth.Domain.Model;
using Yunt.Common;

namespace Yunt.Auth.Repository.EF.Repositories
{
    public class TbCategoryRepository :TaskRepositoryBase<TbCategory,Models.TbCategory>,ITbCategoryRepository
    {

        public TbCategoryRepository(TaskManagerContext context, IMapper mapper) : base(context,mapper)
        {

        }

     

        
    }
}
