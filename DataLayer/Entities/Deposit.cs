using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Deposit
    {
        public Guid DepositId { get; set; }
        public double Amount { get; set; }
        public DateTime DepositDate { get; set; }
        public PaymentMethods Method {get; set;}
        public Guid WalletId { get; set; }

        public virtual Wallet? Wallet { get; set; }
        public virtual ICollection<WalletTransaction>? WalletTransactions { get; set; }
    }
}

public enum PaymentMethods
{
    ZaloPay,
    Momo,
    Visa,
    DebitCard,
    Paypal
}
