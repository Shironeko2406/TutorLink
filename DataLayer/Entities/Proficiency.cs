using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Proficiency
    {
        public int ProficiencyId { get; set; }
        public string ProficiencyCode { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Qualification> Qualifications { get; set; }
    }
}
