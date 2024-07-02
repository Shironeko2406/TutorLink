using DataLayer.Entities;

namespace DataLayer.DAL.Repositories;

public class ApplyRepository : GenericRepository<Apply>
{
    public ApplyRepository(TutorDbContext context) : base(context) { }
    public Apply GetById(Guid id)
    {
        try
        {
            return _dbSet.SingleOrDefault(a => a.ApplyId == id);
        }
        catch (Exception ex)
        {
            // Log the exception if necessary
            throw new Exception($"Error retrieving apply by ID: {ex.Message}", ex);
        }
    }
}