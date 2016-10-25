using System.Data.Entity;
using Model.CompanyInfo;
using Model.Investment;

namespace Data.Helper
{
    public class DataServiceContext : DbContext
    {
        public DataServiceContext() : base("name=ModernInfoTechInventoryContext")
        {
        }

        public DbSet<CompanyInfoEntity> CompanyInfo { get; set; }
        public DbSet<InvestmentEntity> Investment { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }
    }
}