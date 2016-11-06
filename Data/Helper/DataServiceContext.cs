using System.Data.Entity;
using Model.CompanyInfo;
using Model.Customer;
using Model.Investment;
using Model.Inventory;
using Model.Investor;
using Model.Location;
using Model.Supplier;
using Model.Product;
using Model.Purchase;
using Model.InvoiceInfo;
using Model.Sale;

namespace Data.Helper
{
    public class DataServiceContext : DbContext
    {
        public DataServiceContext() : base("name=ModernInfoTechInventoryContext")
        {
        }

        public DbSet<CompanyInfoEntity> CompanyInfo { get; set; }
        public DbSet<InvestmentEntity> Investment { get; set; }
        public DbSet<LocationEntity> Location { get; set; }
        public DbSet<SupplierEntity> Supplier { get; set; }
        public DbSet<SupplierPaymentEntity> SupplierPayment { get; set; }
        public DbSet<CustomerEntity> Customer { get; set; }
        public DbSet<CustomerDueEntity> CustomerDue { get; set; }
        public DbSet<CategoryEntity> Category { get; set; }
        public DbSet<SubCategoryEntity> SubCategory { get; set; }
        public DbSet<ProductInfoEntity> ProductInfo { get; set; }
        public DbSet<ProductQuantityEntity> ProductQuantity { get; set; }
        public DbSet<StockAdjustmentEntity> StockAdjustment { get; set; }
        public DbSet<DamageStockEntryEntity> DamageStockEntry { get; set; }
        public DbSet<SaleReturnQuantityEntity> SaleReturnQuantity { get; set; }
        public DbSet<InvoiceInfoEntity> InvoiceInfo { get; set; }
        public DbSet<InventoryEntity> Inventory { get; set; }
        public DbSet<BillEntryEntity> BillEntry { get; set; }
        public DbSet<CashEntity> Cash { get; set; }
        public DbSet<ExpenseEntity> Expense { get; set; }
        public DbSet<VatEntity> Vat { get; set; }
        public DbSet<InvestorEntity> Investor { get; set; }
        public DbSet<InvestorTransactionEntity> InvestorTransaction { get; set; }
        public DbSet<PurchaseEntryEntity> PurchaseEntry { get; set; }
        public DbSet<PurchaseReturnEntity> PurchaseReturn { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }
    }
}