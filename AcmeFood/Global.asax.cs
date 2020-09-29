using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.Unity;


using Acme.Db.Provider;
using Acme.Db.Provider.Entities;
using Acme.Db.Provider.Repositories;
using Acme.Db.Provider.InMemory;
using Acme.Db.Provider.InMemory.Repositories;

namespace AcmeFood
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var _dbContext = new DbContext();
            DependencyResolver.Current.GetService<IUnityContainer>().RegisterInstance<IDbContext>(_dbContext);
            DependencyResolver.Current.GetService<IUnityContainer>().RegisterType<IProductRepository, ProductRepository>();
            DependencyResolver.Current.GetService<IUnityContainer>().RegisterType<ICartRepository, CartRepository>();
            DependencyResolver.Current.GetService<IUnityContainer>().RegisterType<ICashPoolRepository, CashPoolRepository>();
            DependencyResolver.Current.GetService<IUnityContainer>().RegisterType<ICashDenomRepository, CashDenomRepository>();

            var _repoProduct = DependencyResolver.Current.GetService<IProductRepository>();
            _repoProduct.DataInitilization();
        }
    }
}
