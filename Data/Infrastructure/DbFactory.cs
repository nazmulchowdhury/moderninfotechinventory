using Data.Helper;

namespace Data.Infrastructure
{
    public class DbFactory : IDbFactory
    {
        private DataServiceContext dbContext;

        public DataServiceContext Init()
        {
            return dbContext ?? (dbContext = new DataServiceContext());
        }
    }
}
