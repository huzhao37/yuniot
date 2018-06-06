using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
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
            dynamic x = (new AutoMapperProfileConfiguration()).GetType();
            string currentpath = Path.GetDirectoryName(x.Assembly.Location);
            var sTypes = AssemblyLoadContext.Default.LoadFromAssemblyPath($"{currentpath}\\Yunt.Inventory.Repository.EF.dll").GetSubclasses(typeof(BaseModel));
            // Assembly.LoadFrom($"{currentpath}\\Yunt.Inventory.Repository.EF.dll").GetSubclasses(typeof(BaseModel));
            var dTypes = AssemblyLoadContext.Default.LoadFromAssemblyPath($"{currentpath}\\Yunt.Inventory.Domain.dll").GetSubclasses(typeof(AggregateRoot));
            //Assembly.LoadFrom($"{currentpath}\\Yunt.Inventory.Domain.dll").GetSubclasses(typeof(AggregateRoot));

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
