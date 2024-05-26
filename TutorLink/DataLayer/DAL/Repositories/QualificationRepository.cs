using DataLayer.Entities;

namespace DataLayer.DAL.Repositories;

public class QualificationRepository : GenericRepository<Qualification>
{
    public QualificationRepository(TutorDbContext context) : base(context){}
}