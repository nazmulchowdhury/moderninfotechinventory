using System.Web.Http;
using Data.Infrastructure;
using Data.Repositories.CompanyInfo;
using Data.Repositories.Vat;
using Data.Repositories.Location;
using Data.Repositories.Supplier;
using Data.Repositories.Customer;
using Data.Repositories.InvoiceInfo;
using Data.Repositories.Inventory;
using Data.Repositories.Sale;
using Data.Repositories.Account;
using Data.Repositories.Purchase;
using Data.Repositories.Requisition;
using Data.Repositories.DeliveryOrder;
using Data.Repositories.Utilities;
using Service.CompanyInfo;
using Service.Vat;
using Service.Location;
using Service.Supplier;
using Service.Customer;
using Service.InvoiceInfo;
using Service.Inventory;
using Service.Sale;
using Service.Purchase;
using Service.Requisition;
using Service.DeliveryOrder;
using Service.Utilities;
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
            container.RegisterType<IProductReturnQuantityRepository, ProductReturnQuantityRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IProductReturnQuantityServices, ProductReturnQuantityServices>(new HierarchicalLifetimeManager());
            container.RegisterType<IStockAdjustmentRepository, StockAdjustmentRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IStockAdjustmentServices, StockAdjustmentServices>(new HierarchicalLifetimeManager());
            container.RegisterType<IDamageStockEntryRepository, DamageStockEntryRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IDamageStockEntryServices, DamageStockEntryServices>(new HierarchicalLifetimeManager());
            container.RegisterType<ISaleReturnRepository, SaleReturnRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ISaleReturnServices, SaleReturnServices>(new HierarchicalLifetimeManager());
            container.RegisterType<IInvoiceInfoRepository, InvoiceInfoRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IInvoiceInfoServices, InvoiceInfoServices>(new HierarchicalLifetimeManager());
            container.RegisterType<IInventoryRepository, InventoryRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IInventoryServices, InventoryServices>(new HierarchicalLifetimeManager());
            container.RegisterType<ICashRepository, CashRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ICashServices, CashServices>(new HierarchicalLifetimeManager());
            container.RegisterType<IExpenseRepository, ExpenseRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IExpenseServices, ExpenseServices>(new HierarchicalLifetimeManager());
            container.RegisterType<IVatRepository, VatRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IVatServices, VatServices>(new HierarchicalLifetimeManager());
            container.RegisterType<IInvestorRepository, InvestorRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IInvestorServices, InvestorServices>(new HierarchicalLifetimeManager());
            container.RegisterType<IInvestorTransactionRepository, InvestorTransactionRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IInvestorTransactionServices, InvestorTransactionServices>(new HierarchicalLifetimeManager());
            container.RegisterType<IPurchaseEntryRepository, PurchaseEntryRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IPurchaseEntryServices, PurchaseEntryServices>(new HierarchicalLifetimeManager());
            container.RegisterType<IPurchaseReturnRepository, PurchaseReturnRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IPurchaseReturnServices, PurchaseReturnServices>(new HierarchicalLifetimeManager());
            container.RegisterType<IBillEntryRepository, BillEntryRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IBillEntryServices, BillEntryServices>(new HierarchicalLifetimeManager());
            container.RegisterType<IRequisitionRepository, RequisitionRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IRequisitionServices, RequisitionServices>(new HierarchicalLifetimeManager());
            container.RegisterType<IDeliveryOrderRepository, DeliveryOrderRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IDeliveryOrderServices, DeliveryOrderServices>(new HierarchicalLifetimeManager());
            container.RegisterType<IUnitRepository, UnitRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IUnitServices, UnitServices>(new HierarchicalLifetimeManager());
            container.RegisterType<ICurrentStockRepository, CurrentStockRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ICurrentStockServices, CurrentStockServices>(new HierarchicalLifetimeManager());

            config.DependencyResolver = new UnityResolver(container);
        }
    }
}