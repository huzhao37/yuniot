using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using AutoMapper;
using Yunt.Auth.Domain.BaseModel;
using Yunt.Auth.Repository.EF.Models;
using TbCategory = Yunt.Auth.Domain.Model.TbCategory;

namespace Yunt.Auth.Repository.EF
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public void Configure()
        {

            CreateMap<AggregateRoot,BaseModel>();
            CreateMap<BaseModel, AggregateRoot>();

            CreateMap<IEnumerable<AggregateRoot>, IEnumerable<BaseModel>>();
            CreateMap<IEnumerable<BaseModel>, IEnumerable<AggregateRoot>>();

            CreateMap<IQueryable<BaseModel>, IQueryable<AggregateRoot>>();
            CreateMap<IQueryable<AggregateRoot>, IQueryable<BaseModel>>();

            CreateMap<List<BaseModel>, List<AggregateRoot>>();
            CreateMap<List<AggregateRoot>, List<BaseModel>>();


            CreateMap<TbCategory, Models.TbCategory>();
            CreateMap<Models.TbCategory, TbCategory>();

            CreateMap<IEnumerable<TbCategory>, IEnumerable<Models.TbCategory>>();
            CreateMap<IEnumerable<Models.TbCategory>,IEnumerable<TbCategory>>();

            CreateMap<IQueryable<TbCategory>, IQueryable<Models.TbCategory>>();
            CreateMap<IQueryable<Models.TbCategory>, IQueryable<TbCategory>>();

            CreateMap<List<TbCategory>, List<Models.TbCategory>>();
            CreateMap<List<Models.TbCategory>, List<TbCategory>>();

           
            //todo
        }
    }
}
