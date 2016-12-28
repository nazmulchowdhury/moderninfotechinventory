using Data.Helper;
using System.Linq;
using Model.InvoiceInfo;
using Data.Infrastructure;

namespace Data.Repositories.InvoiceInfo
{
    public class InvoiceInfoRepository : RepositoryBase<InvoiceInfoEntity>, IInvoiceInfoRepository
    {
        public InvoiceInfoRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }

        public override InvoiceInfoEntity GetById(string invoiceInfoId)
        {
            return Context.InvoiceInfo.Include("TenantInfo").FirstOrDefault(invinf => invinf.InvoiceInfoId == invoiceInfoId);
        }

        public override bool Delete(string invoiceInfoId)
        {
            var invoiceInfoEntity = Context.InvoiceInfo.Find(invoiceInfoId);
            if (invoiceInfoEntity != null)
            {
                var tenantEntity = Context.Tenant.Find(invoiceInfoEntity.TenantId);
                if (tenantEntity != null)
                {
                    Context.Tenant.Remove(tenantEntity);
                }
                Context.InvoiceInfo.Remove(invoiceInfoEntity);
                Context.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
