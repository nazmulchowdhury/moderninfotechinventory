using System.Collections.Generic;
using Data.Repositories.Investment;
using Model.Investment;

namespace Service.Investment
{
    public class ExpenseServices : IExpenseServices
    {
        private readonly IExpenseRepository expenseRepository;

        public ExpenseServices(IExpenseRepository expenseRepository)
        {
            this.expenseRepository = expenseRepository;
        }

        public IEnumerable<ExpenseEntity> GetAllExpenses()
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
