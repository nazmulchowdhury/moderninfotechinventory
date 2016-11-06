using System.Collections.Generic;
using Model.Investment;

namespace Service.Investment
{
    public interface IExpenseServices
    {
        IEnumerable<ExpenseEntity> GetAllExpenses();
        ExpenseEntity GetExpense(string expenseId);
        ExpenseEntity CreateExpense(ExpenseEntity expenseEntity);
        bool DeleteExpense(string expenseId);
    }
}
