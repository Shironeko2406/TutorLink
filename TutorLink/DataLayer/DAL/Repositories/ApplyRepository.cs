using DataLayer.Entities;

namespace DataLayer.DAL.Repositories;

public class ApplyRepository : GenericRepository<Apply>
{
    public ApplyRepository(TutorDbContext context) : base(context){}
}