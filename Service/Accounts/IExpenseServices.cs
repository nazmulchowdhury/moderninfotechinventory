using System.Collections.Generic;
using Model.Accounts;

namespace Service.Vat
{
    public interface IExpenseServices
    {
        ICollection<ExpenseEntity> GetAllExpenses();
        ExpenseEntity GetExpense(string expenseId);
        ExpenseEntity CreateExpense(ExpenseEntity expenseEntity);
        bool DeleteExpense(string expenseId);
    }
}
