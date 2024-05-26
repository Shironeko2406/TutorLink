using DataLayer.Entities;

namespace DataLayer.DAL.Repositories;

public class AccountRepository : GenericRepository<Account>
{
    public AccountRepository(TutorDbContext context) : base(context){}
}