using DataLayer.Entities;

namespace DataLayer.DAL.Repositories;

public class PostRequestRepository : GenericRepository<PostRequest>
{
    public PostRequestRepository(TutorDbContext context) : base(context){}
}