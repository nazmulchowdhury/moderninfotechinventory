using System;

namespace ModernInfoTechInventory.ViewModels.Accounts
{
    public class InvestmentView
    {
        public double Amount { get; set; }
    }

    public class CashView
    {
        public double Amount { get; set; }
    }

    public class ExpenseView
    {
        public DateTime ExpenseDate { get; set; }

        public string Purpose { get; set; }

        public double Amount { get; set; }

        public string ExpensedBy { get; set; }
    }

    public class InvestorView
    {
        public string InvestorName { get; set; }

        public string LocationId { get; set; }

        public string PhoneNumber { get; set; }

        public double Balance { get; set; }
    }

    public class InvestorTransactionView
    {
        public DateTime TransactionDate { get; set; }

        public double Amount { get; set; }

        public string Description { get; set; }

        public bool TransactionType { get; set; }

        public string InvestorId { get; set; }
    }
}