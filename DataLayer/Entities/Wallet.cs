namespace DataLayer.Entities;

public class Wallet
{
    public Guid WalletId { get; set; }
    public double Balance { get; set; }
    public Guid TutorId { get; set; }
    
    public virtual Tutor? Tutor { get; set; }
    public virtual ICollection<WalletTransaction>? WalletTransactions  { get; set; }
    public virtual ICollection<Deposit>? Deposits { get; set; }
}