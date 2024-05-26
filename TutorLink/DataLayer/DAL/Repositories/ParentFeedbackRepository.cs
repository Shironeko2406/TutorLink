using DataLayer.Entities;

namespace DataLayer.DAL.Repositories;

public class ParentFeedbackRepository : GenericRepository<ParentFeedback>       
{
    public ParentFeedbackRepository(TutorDbContext context) : base(context){}
}