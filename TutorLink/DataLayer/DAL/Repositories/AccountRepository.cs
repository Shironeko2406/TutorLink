using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.DAL.Repositories;

public class AccountRepository : GenericRepository<Account>
{
    public AccountRepository(TutorDbContext context) : base(context){}
    public Account GetById(Guid id)
    {
        return _dbSet.Find(id);
    }

}