using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using ModernInfoTechInventory.Resolver;
using Data.Infrastructure;
using Data.Repositories.CompanyInfo;
using Data.Repositories.Investment;
using Service.CompanyInfo;
using Service.Investment;
using ModernInfoTechInventory.ActionFilters;
using Microsoft.Practices.Unity;

namespace ModernInfoTechInventory
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = new UnityContainer();
            container.RegisterType<IDbFactory, DbFactory>(new HierarchicalLifetimeManager());
            container.RegisterType<ICompanyInfoRepositoy, CompanyInfoRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ICompanyInfoServices, CompanyInfoServices>(new HierarchicalLifetimeManager());
            container.RegisterType<IInvestmentRepository, InvestmentRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IInvestmentServices, InvestmentServices>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            config.Filters.Add(new LoggingFilterAttribute());
            config.Filters.Add(new GlobalExceptionAttribute());

            // Use camel case for JSON data.
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
