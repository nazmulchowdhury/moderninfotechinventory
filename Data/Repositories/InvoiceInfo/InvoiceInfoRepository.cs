﻿using Data.Infrastructure;
using Data.Helper;
using Model.InvoiceInfo;

namespace Data.Repositories.InvoiceInfo
{
    public class InvoiceInfoRepository : RepositoryBase<InvoiceInfoEntity>, IInvoiceInfoRepository
    {
        public InvoiceInfoRepository(IDbFactory dbFactory) : base(dbFactory)
        { }
    }
}
