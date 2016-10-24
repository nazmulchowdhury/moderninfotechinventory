using Data.Helper;

namespace Data.Infrastructure
{
    public interface IDbFactory
    {
        DataServiceContext Init();
    }
}
