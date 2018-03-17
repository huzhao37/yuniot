using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using AutoMapper;
using Yunt.Device.Domain.BaseModel;
using Yunt.Device.Repository.EF.Models;

namespace Yunt.Device.Repository.EF
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


            //以反射形式建立映射关系
            CreateMap<Motor, Domain.Model.Motor>();
            CreateMap< Domain.Model.Motor, Motor>();

            CreateMap<IEnumerable<Motor>, IEnumerable< Domain.Model.Motor>>();
            CreateMap<IEnumerable< Domain.Model.Motor>,IEnumerable<Motor>>();

            CreateMap<IQueryable<Motor>, IQueryable< Domain.Model.Motor>>();
            CreateMap<IQueryable< Domain.Model.Motor>, IQueryable<Motor>>();

            CreateMap<List<Motor>, List< Domain.Model.Motor>>();
            CreateMap<List< Domain.Model.Motor>, List<Motor>>();

            CreateMap<Conveyor, Domain.Model.Conveyor>();
            CreateMap<Domain.Model.Conveyor, Conveyor>();

            CreateMap<IEnumerable<Conveyor>, IEnumerable<Domain.Model.Conveyor>>();
            CreateMap<IEnumerable<Domain.Model.Conveyor>, IEnumerable<Conveyor>>();

            CreateMap<IQueryable<Conveyor>, IQueryable<Domain.Model.Conveyor>>();
            CreateMap<IQueryable<Domain.Model.Conveyor>, IQueryable<Conveyor>>();

            CreateMap<List<Conveyor>, List<Domain.Model.Conveyor>>();
            CreateMap<List<Domain.Model.Conveyor>, List<Conveyor>>();


            //todo
        }
    }
}
