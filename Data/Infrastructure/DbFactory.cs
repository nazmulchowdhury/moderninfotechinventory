using Data.Helper;

namespace Data.Infrastructure
{
    public class DbFactory : IDbFactory
    {
        private ModernInfoTechInventoryContext context;

        public ModernInfoTechInventoryContext Init()
        {
            return context ?? (context = new ModernInfoTechInventoryContext());
        }
    }
}
