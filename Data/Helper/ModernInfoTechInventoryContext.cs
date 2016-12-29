using Model.Sale;
using Model.Customer;
using Model.Accounts;
using Model.Location;
using Model.Supplier;
using Model.Purchase;
using Model.Tenant;
using Model.Inventory;
using Model.Utilities;
using Model.CompanyInfo;
using Model.InvoiceInfo;
using Model.Requisition;
using System.Data.Entity;
using Model.DeliveryOrder;

namespace Data.Helper
{
    public class ModernInfoTechInventoryContext : DbContext
    {
        public ModernInfoTechInventoryContext()
            : base("name=ModernInfoTechInventoryContext")
        { }

        public DbSet<TenantEntity> Tenant { get; set; }
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
        public DbSet<ProductReturnQuantityEntity> ProductReturnQuantity { get; set; }
        public DbSet<StockAdjustmentEntity> StockAdjustment { get; set; }
        public DbSet<DamageStockEntryEntity> DamageStockEntry { get; set; }
        public DbSet<SaleReturnEntity> SaleReturn { get; set; }
        public DbSet<InvoiceInfoEntity> InvoiceInfo { get; set; }
        public DbSet<InventoryEntity> Inventory { get; set; }
        public DbSet<SaleEntryEntity> SaleEntry { get; set; }
        public DbSet<CashEntity> Cash { get; set; }
        public DbSet<ExpenseEntity> Expense { get; set; }
        public DbSet<VatEntity> Vat { get; set; }
        public DbSet<InvestorEntity> Investor { get; set; }
        public DbSet<InvestorTransactionEntity> InvestorTransaction { get; set; }
        public DbSet<PurchaseEntryEntity> PurchaseEntry { get; set; }
        public DbSet<PurchaseReturnEntity> PurchaseReturn { get; set; }
        public DbSet<RequisitionEntity> Requisition { get; set; }
        public DbSet<DeliveryOrderEntity> DeliveryOrder { get; set; }
        public DbSet<StockedProductQuantity> StockedProductQuantity { get; set; }
        public DbSet<StockedProductReturnQuantity> StockedProductReturnQuantity { get; set; }
        public DbSet<UnitEntity> Unit { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SaleEntryEntity>()
                .HasMany(sale => sale.SaledProducts)
                .WithMany(proqty => proqty.SaleEntries)
                .Map(bp =>
                    bp.MapLeftKey("SaleEntryId")
                    .MapRightKey("ProductQuantityId")
                    .ToTable("SaleEntryProductQuantity"));

            modelBuilder.Entity<PurchaseEntryEntity>()
                .HasMany(purchase => purchase.PurchasedProducts)
                .WithMany(proqty => proqty.PurchaseEntries)
                .Map(pp =>
                    pp.MapLeftKey("PurchaseEntryId")
                    .MapRightKey("ProductQuantityId")
                    .ToTable("PurchaseEntryProductQuantity"));

            modelBuilder.Entity<SaleReturnEntity>()
                .HasMany(salereturn => salereturn.SaleReturnedProducts)
                .WithMany(prortnqty => prortnqty.SaleReturns)
                .Map(sp =>
                    sp.MapLeftKey("SaleReturnId")
                    .MapRightKey("ProductReturnQuantityId")
                    .ToTable("SaleReturnQuantity"));

            modelBuilder.Entity<PurchaseReturnEntity>()
                .HasMany(purchasereturn => purchasereturn.PurchaseReturnedProducts)
                .WithMany(prortnqty => prortnqty.PurchaseReturns)
                .Map(prp =>
                    prp.MapLeftKey("PurchaseReturnId")
                    .MapRightKey("ProductReturnQuantityId")
                    .ToTable("PurchaseReturnQuantity"));

            modelBuilder.Entity<StockAdjustmentEntity>()
                .HasMany(stkadj => stkadj.ProductQuantities)
                .WithMany(proqty => proqty.Stocks)
                .Map(stkadjpq =>
                stkadjpq.MapLeftKey("StockAdjustmentId")
                .MapRightKey("ProductQuantityId")
                .ToTable("StockAdjustmentProductQuantity"));

            modelBuilder.Entity<RequisitionEntity>()
                .HasMany(requisition => requisition.ProductQuantities)
                .WithMany(proqty => proqty.Requisitions)
                .Map(reqpqty =>
                reqpqty.MapLeftKey("RequisitionId")
                .MapRightKey("ProductQuantityId")
                .ToTable("RequisitionProductQuantity"));

            modelBuilder.Entity<DeliveryOrderEntity>()
                .HasMany(deliverorder => deliverorder.ProductQuantities)
                .WithMany(proqty => proqty.DeliveryOrders)
                .Map(delodrpqt =>
                delodrpqt.MapLeftKey("DeliveryOrderId")
                .MapRightKey("ProductQuantityId")
                .ToTable("DeliveryOrderProductQuantity"));
        }
    }
}