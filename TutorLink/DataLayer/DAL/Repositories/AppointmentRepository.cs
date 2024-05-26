using DataLayer.Entities;

namespace DataLayer.DAL.Repositories;

public class AppointmentRepository : GenericRepository<Appointment>
{
    public AppointmentRepository(TutorDbContext context) : base(context){}
}