using DataLayer.Entities;

namespace DataLayer.DAL.Repositories;

public class TutorRepository : GenericRepository<Tutor>
{
    public TutorRepository(TutorDbContext context)  : base(context) {}
}