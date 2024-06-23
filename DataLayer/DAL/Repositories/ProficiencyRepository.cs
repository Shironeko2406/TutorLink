using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DAL.Repositories
{
    public class ProficiencyRepository : GenericRepository<Proficiency>
    {
        public ProficiencyRepository(TutorDbContext context) : base(context) { }
    }
}
