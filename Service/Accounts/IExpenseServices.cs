using Model.Accounts;
using System.Collections.Generic;

namespace Service.Accounts
{
    public interface IExpenseServices
    {
        ICollection<ExpenseEntity> GetAllExpenses();
        ExpenseEntity GetExpense(string expenseId);
        ExpenseEntity CreateExpense(ExpenseEntity expenseEntity);
        bool DeleteExpense(string expenseId);
    }
}
