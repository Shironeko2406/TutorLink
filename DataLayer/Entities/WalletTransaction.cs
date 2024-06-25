namespace DataLayer.Entities;

public class WalletTransaction
{
    public Guid TransactionId { get; set; }
    public double Amount { get; set; }
    public TransactionTypes Type { get; set; }  
    public DateTime TransactionDate { get; set; }
    public Guid WalletId { get; set; }
    public Guid? DepositId { get; set; }
    
    public virtual Wallet? Wallet { get; set; }
    public virtual Deposit? Deposit { get; set; }
}
public enum TransactionTypes
{
    Deposit,
    Withdraw
}