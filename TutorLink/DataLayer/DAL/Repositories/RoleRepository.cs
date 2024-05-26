using DataLayer.Entities;

namespace DataLayer.DAL.Repositories;

public class RoleRepository : GenericRepository<Role>
{
    public RoleRepository(TutorDbContext context) : base(context){}
}