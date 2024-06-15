using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.DAL.Repositories;

public class AccountRepository : GenericRepository<Account>
{
    public AccountRepository(TutorDbContext context) : base(context){}
    public Account GetById(Guid id)
    {
        try
        {
            return _dbSet.SingleOrDefault(a => a.AccountId == id);
        }
        catch (Exception ex)
        {
            // Log the exception if necessary
            throw new Exception($"Error retrieving account by ID: {ex.Message}", ex);
        }
    }
    public IEnumerable<Account> GetAll()
    {
        try
        {
            return _dbSet.ToList();
        }
        catch (Exception ex)
        {
            // Log the exception if necessary
            throw new Exception($"Error retrieving all accounts: {ex.Message}", ex);
        }
    }

}