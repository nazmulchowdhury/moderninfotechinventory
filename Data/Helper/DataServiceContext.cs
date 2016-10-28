using System.Data.Entity;
using Model.CompanyInfo;
using Model.Customer;
using Model.Investment;
using Model.Location;
using Model.Supplier;

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

        public virtual void Commit()
        {
            base.SaveChanges();
        }
    }
}