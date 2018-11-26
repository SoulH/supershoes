using Core.Interfaces;
using Core.Services;
using Infrastructure;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace Api
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            container.RegisterType<IRepository, Repository>();
            container.RegisterType<IStoreService, StoreService>();
            container.RegisterType<IArticleService, ArticleService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}