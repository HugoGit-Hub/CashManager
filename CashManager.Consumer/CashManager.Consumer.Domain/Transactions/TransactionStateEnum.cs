using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashManager.Consumer.Domain.Transaction;

public enum TransactionStateEnum
{
    Success,
    Pending,
    Aborted
}
