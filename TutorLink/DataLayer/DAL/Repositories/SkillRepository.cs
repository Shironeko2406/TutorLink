using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DAL.Repositories
{
    public class SkillRepository : GenericRepository<Skill>
    {
        public SkillRepository(TutorDbContext context) : base(context) { }
    }
}
