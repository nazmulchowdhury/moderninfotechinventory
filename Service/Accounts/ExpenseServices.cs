using System.Collections.Generic;
using Data.Repositories.Vat;
using Model.Accounts;

namespace Service.Vat
{
    public class ExpenseServices : IExpenseServices
    {
        private readonly IExpenseRepository expenseRepository;

        public ExpenseServices(IExpenseRepository expenseRepository)
        {
            this.expenseRepository = expenseRepository;
        }

        public ICollection<ExpenseEntity> GetAllExpenses()
        {
            return expenseRepository.GetAll();
        }

        public ExpenseEntity GetExpense(string expenseId)
        {
            return expenseRepository.GetById(expenseId);
        }

        public ExpenseEntity CreateExpense(ExpenseEntity expenseEntity)
        {
            return expenseRepository.Add(expenseEntity);
        }

        public bool DeleteExpense(string expenseId)
        {
            return expenseRepository.Delete(expenseId);
        }
    }
}
