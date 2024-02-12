using Autofac;
using Autofac.Integration.WebApi;
using CarRent.Repository;
using CarRent.Repository.Common;
using CarRent.Service;
using CarRent.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace CarRent.WebApi
{
    public static class DependencyInjectionConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<CarService>().As<ICarService>();
            builder.RegisterType<CarRepository>().As<ICarRepository>();
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
