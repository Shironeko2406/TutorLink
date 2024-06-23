using DataLayer.Entities;

namespace DataLayer.DAL.Repositories;

public class WalletRepository : GenericRepository<Wallet>
{
    public WalletRepository(TutorDbContext context) : base(context){}
}