using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using NewLife.Reflection;
using Yunt.Inventory.Domain.BaseModel;
using Yunt.Inventory.Repository.EF.Models;

namespace Yunt.Inventory.Repository.EF
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public void Configure()
        {
            CreateMap<AggregateRoot,BaseModel>();
            CreateMap<BaseModel, AggregateRoot>();

            CreateMap<IEnumerable<AggregateRoot>, IEnumerable<BaseModel>>();
            CreateMap<IEnumerable<BaseModel>, IEnumerable<AggregateRoot>>();

            CreateMap<IQueryable<AggregateRoot>, IQueryable<BaseModel>>();
            CreateMap<IQueryable<BaseModel>, IQueryable<AggregateRoot>>();

            CreateMap<List<AggregateRoot>, List<BaseModel>>();
            CreateMap<List<BaseModel>, List<AggregateRoot>>();


            //以反射形式建立映射关系
            //todo

            var sTypes = Assembly.LoadFrom("Yunt.Inventory.Repository.EF").GetSubclasses(typeof(BaseModel));

            var dTypes = Assembly.LoadFrom("Yunt.Inventory.Domain").GetSubclasses(typeof(AggregateRoot));
            dTypes.ToList().ForEach(d =>
            {
                sTypes.ToList().ForEach(s =>
                {
                    if (d.Name.Equals(s.Name))
                    {


                        CreateMap(s, d);
                        CreateMap(d, s);
                    }
                });
            });

        }
    }
}
