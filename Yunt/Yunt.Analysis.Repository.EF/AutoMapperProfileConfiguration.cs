using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using AutoMapper;
using NewLife.Reflection;
using Yunt.Analysis.Domain.BaseModel;
using Yunt.Analysis.Repository.EF.Models;

namespace Yunt.Analysis.Repository.EF
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
            //todo

            //var sTypes = Assembly.LoadFrom("Yunt.Analysis.Repository.EF").GetSubclasses(typeof(BaseModel));

            //var dTypes = Assembly.LoadFrom("Yunt.Analysis.Domain").GetSubclasses(typeof(AggregateRoot));
            dynamic x = (new AutoMapperProfileConfiguration()).GetType();
            string currentpath = Path.GetDirectoryName(x.Assembly.Location);
            var sTypes = AssemblyLoadContext.Default.LoadFromAssemblyPath($"{currentpath}\\Yunt.Analysis.Repository.EF.dll").GetSubclasses(typeof(BaseModel));
            // Assembly.LoadFrom($"{currentpath}\\Yunt.Analysis.Repository.EF.dll").GetSubclasses(typeof(BaseModel));
            var dTypes = AssemblyLoadContext.Default.LoadFromAssemblyPath($"{currentpath}\\Yunt.Analysis.Domain.dll").GetSubclasses(typeof(AggregateRoot));
            //Assembly.LoadFrom($"{currentpath}\\Yunt.Analysis.Domain.dll").GetSubclasses(typeof(AggregateRoot));
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


            //todo
        }
    }
}
