using System.Data.Entity;
using Model.CompanyInfo;

namespace Data.Helper
{
    public class DataServiceContext : DbContext
    {
        public DataServiceContext() : base("name=ModernInfoTechInventoryContext")
        {
        }

        public DbSet<CompanyInfoEntity> CompanyInfo { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }
    }
}