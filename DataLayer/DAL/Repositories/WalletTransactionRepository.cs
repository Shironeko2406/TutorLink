using DataLayer.Entities;

namespace DataLayer.DAL.Repositories;

public class WalletTransactionRepository : GenericRepository<WalletTransaction>
{
    public WalletTransactionRepository(TutorDbContext context)  : base(context){}
}