using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using BusinessLayerLibrary.DAL;
using BusinessLayerLibrary.Facades;
using BusinessLayerLibrary.Infrastructure;

namespace WebAuthForm
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var builder = new ContainerBuilder();

            builder.RegisterType<Config>().As<IDbConfig>().InstancePerDependency();
            builder.RegisterType<UserFacade>().InstancePerDependency();
            
            builder.RegisterType<DataManagerFactory>().As<IDataManagerFactory>().InstancePerDependency();
            builder.RegisterType<SHA1CryptoServiceProvider>().As<HashAlgorithm>().InstancePerDependency();
            builder.RegisterControllers(typeof(MvcApplication).Assembly).InstancePerDependency();
            IContainer ioC = builder.Build();
            var res = new Autofac.Integration.Mvc.AutofacDependencyResolver(ioC);
            DependencyResolver.SetResolver(res);

        }
    }
}
