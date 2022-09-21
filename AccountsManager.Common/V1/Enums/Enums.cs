using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager.Common.V1.Enums
{
    public enum AccountType
    {
        Assets = 1,
        Liability = 2,
        OwnerEquity = 3,
        Revenue = 4,
        Expense = 5,
    }
    public enum VoucherType
    {
        PaymentVoucher = 1,
        JournalVoucher = 2,
        ReceiptVoucher = 3,
    }
}
