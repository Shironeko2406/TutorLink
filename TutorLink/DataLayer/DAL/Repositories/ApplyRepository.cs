using DataLayer.Entities;
using DataLayer.DAL.Repositories;
using DataLayer.DAL;

public class ApplyRepository : GenericRepository<Apply>
{
    public ApplyRepository(TutorDbContext context) : base(context)
    {
    }
}
