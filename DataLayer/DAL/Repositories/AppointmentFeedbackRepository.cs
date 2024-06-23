using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DAL.Repositories
{
    public class AppointmentFeedbackRepository : GenericRepository<AppointmentFeedback>
    {
        public AppointmentFeedbackRepository(TutorDbContext context) : base(context) { }
    }
}
