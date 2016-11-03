using System.Web.Http;
using Data.Infrastructure;
using Data.Repositories.CompanyInfo;
using Data.Repositories.Investment;
using Data.Repositories.Location;
using Data.Repositories.Supplier;
using Data.Repositories.Customer;
using Data.Repositories.InvoiceInfo;
using Data.Repositories.Product.Category;
using Data.Repositories.Product.SubCategory;
using Data.Repositories.Product.ProductInfo;
using Data.Repositories.Product.StockAdjustment;
using Data.Repositories.Product.DamageStockEntry;
using Data.Repositories.Sale.SaleReturn;
using Service.CompanyInfo;
using Service.Investment;
using Service.Location;
using Service.Supplier;
using Service.Customer;
using Service.InvoiceInfo;
using Service.Product.Category;
using Service.Product.SubCategory;
using Service.Product.ProductInfo;
using Service.Product.StockAdjustment;
using Service.Product.DamageStockEntry;
using Service.Sale.SaleReturn;
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
            container.RegisterType<ICategoryRepository, CategoryRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ICategoryServices, CategoryServices>(new HierarchicalLifetimeManager());
            container.RegisterType<ISubCategoryRepository, SubCategoryRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ISubCategoryServices, SubCategoryServices>(new HierarchicalLifetimeManager());
            container.RegisterType<IProductInfoRepository, ProductInfoRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IProductInfoServices, ProductInfoServices>(new HierarchicalLifetimeManager());
            container.RegisterType<IProductQuantityRepository, ProductQuantityRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IProductQuantityServices, ProductQuantityServices>(new HierarchicalLifetimeManager());
            container.RegisterType<IStockAdjustmentRepository, StockAdjustmentRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IStockAdjustmentServices, StockAdjustmentServices>(new HierarchicalLifetimeManager());
            container.RegisterType<IDamageStockEntryRepository, DamageStockEntryRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IDamageStockEntryServices, DamageStockEntryServices>(new HierarchicalLifetimeManager());
            container.RegisterType<ISaleReturnQuantityRepository, SaleReturnQuantityRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ISaleReturnQuantityServices, SaleReturnQuantityServices>(new HierarchicalLifetimeManager());
            container.RegisterType<IInvoiceInfoRepository, InvoiceInfoRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IInvoiceInfoServices, InvoiceInfoServices>(new HierarchicalLifetimeManager());

            config.DependencyResolver = new UnityResolver(container);
        }
    }
}