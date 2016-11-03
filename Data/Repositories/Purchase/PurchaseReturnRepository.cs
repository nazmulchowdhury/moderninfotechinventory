using Data.Infrastructure;
using Data.Helper;
using Model.Purchase;
using System.Linq;

namespace Data.Repositories.Purchase
{
    public class PurchaseReturnRepository : RepositoryBase<PurchaseReturnEntity>, IPurchaseReturnRepository
    {
        public PurchaseReturnRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        public override PurchaseReturnEntity GetById(string purchaseReturnId)
        {
            return DbContext.PurchaseReturn.Include("PurchaseEntry").FirstOrDefault(purrtn => purrtn.PurchaseReturnId == purchaseReturnId);
        }
    }
}
