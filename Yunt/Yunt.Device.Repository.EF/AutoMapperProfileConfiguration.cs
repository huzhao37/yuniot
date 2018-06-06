using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using AutoMapper;
using NewLife.Reflection;
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




            //todo

            //var sTypes = Assembly.LoadFrom("Yunt.Device.Repository.EF").GetSubclasses(typeof(BaseModel));


            //var dTypes = Assembly.LoadFrom("Yunt.Device.Domain").GetSubclasses(typeof(AggregateRoot));//.GetTypes()
            dynamic x = (new AutoMapperProfileConfiguration()).GetType();
            string currentpath = Path.GetDirectoryName(x.Assembly.Location);

            var sTypes = AssemblyLoadContext.Default.LoadFromAssemblyPath($"{currentpath}\\Yunt.Device.Repository.EF.dll").GetSubclasses(typeof(BaseModel));
            // Assembly.LoadFrom($"{currentpath}\\Yunt.Device.Repository.EF.dll").GetSubclasses(typeof(BaseModel));
            var dTypes = AssemblyLoadContext.Default.LoadFromAssemblyPath($"{currentpath}\\Yunt.Device.Domain.dll").GetSubclasses(typeof(AggregateRoot));
            //Assembly.LoadFrom($"{currentpath}\\Yunt.Device.Domain.dll").GetSubclasses(typeof(AggregateRoot));
            dTypes.ToList().ForEach(d =>
            {
                sTypes.ToList().ForEach(s =>
                {
                    if (d.Name.Equals(s.Name))
                    {
                        CreateMap(s, d);
                        CreateMap(d, s);
                        //this.CreateMap(s[], d[]);
                        //CreateMap<IEnumerable<si>, IEnumerable<di>>(IEnumerable<s>, IEnumerable<d>);

                        //CreateMap(IEnumerable<Domain.Model.Conveyor>, IEnumerable < Conveyor >);

                        //CreateMap(IQueryable<Conveyor>, IQueryable<Domain.Model.Conveyor>);
                        //CreateMap(IQueryable<Domain.Model.Conveyor>, IQueryable<Conveyor>);

                        //CreateMap(List<Conveyor>, List<Domain.Model.Conveyor>);
                        //CreateMap(List<Domain.Model.Conveyor>, List<Conveyor>);
                    }
                });
            });

        }
    }
}
