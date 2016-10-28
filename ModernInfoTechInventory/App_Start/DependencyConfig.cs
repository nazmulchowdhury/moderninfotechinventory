using System.Web.Http;
using Data.Infrastructure;
using Data.Repositories.CompanyInfo;
using Data.Repositories.Investment;
using Data.Repositories.Location;
using Data.Repositories.Supplier;
using Data.Repositories.Customer;
using Service.CompanyInfo;
using Service.Investment;
using Service.Location;
using Service.Supplier;
using Service.Customer;
using ModernInfoTechInventory.Resolver;
using Microsoft.Practices.Unity;

namespace ModernInfoTechInventory
{
    public class DependencyConfig
    {
        public void ManageDependency(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<IDbFactory, DbFactory>(new HierarchicalLifetimeManager());
            container.RegisterType<ICompanyInfoRepositoy, CompanyInfoRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ICompanyInfoServices, CompanyInfoServices>(new HierarchicalLifetimeManager());
            container.RegisterType<IInvestmentRepository, InvestmentRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IInvestmentServices, InvestmentServices>(new HierarchicalLifetimeManager());
            container.RegisterType<ILocationRepository, LocationRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ILocationServices, LocationServices>(new HierarchicalLifetimeManager());
            container.RegisterType<ISupplierRepository, SupplierRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ISupplierServices, SupplierServices>(new HierarchicalLifetimeManager());
            container.RegisterType<ISupplierPaymentRepository, SupplierPaymentRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ISupplierPaymentServices, SupplierPaymentServices>(new HierarchicalLifetimeManager());
            container.RegisterType<ICustomerRepository, CustomerRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ICustomerServices, CustomerServices>(new HierarchicalLifetimeManager());
            container.RegisterType<ICustomerDueRepository, CustomerDueRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ICustomerDueServices, CustomerDueServices>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);
        }
    }
}