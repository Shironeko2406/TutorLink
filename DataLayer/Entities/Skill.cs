using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Skill
    {
        public int SkillId { get; set; }
        public string SkillName { get; set; }

        public virtual ICollection<Qualification> Qualifications { get; set; }
    }
}
