﻿using System.Collections.Generic;
using Data.Repositories.Purchase;
using Model.Purchase;

namespace Service.Purchase
{
    public class PurchaseReturnServices : IPurchaseReturnServices
    {
        private readonly IPurchaseReturnRepository purchaseReturnRepository;

        public PurchaseReturnServices(IPurchaseReturnRepository purchaseReturnRepository)
        {
            this.purchaseReturnRepository = purchaseReturnRepository;
        }

        public IEnumerable<PurchaseReturnEntity> GetAllPurchaseReturns()
        {
            return purchaseReturnRepository.GetAll();
        }

        public PurchaseReturnEntity GetPurchaseReturn(string purchaseReturnId)
        {
            return purchaseReturnRepository.GetById(purchaseReturnId);
        }

        public PurchaseReturnEntity CreatePurchaseReturn(PurchaseReturnEntity purchaseReturnEntity)
        {
            return purchaseReturnRepository.Add(purchaseReturnEntity);
        }

        public bool UpdatePurchaseReturn(string purchaseReturnId, PurchaseReturnEntity purchaseReturnEntity)
        {
            var storedItem = purchaseReturnRepository.GetById(purchaseReturnId);

            if (storedItem != null)
            {
                storedItem.ReturnDate = purchaseReturnEntity.ReturnDate;
                storedItem.ReturnNumber = purchaseReturnEntity.ReturnNumber;

                purchaseReturnRepository.Update(storedItem);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeletePurchaseReturn(string purchaseReturnId)
        {
            return purchaseReturnRepository.Delete(purchaseReturnId);
        }
    }
}