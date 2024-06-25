using DataLayer.Entities;

namespace TutorLinkAPI.ViewModel
{
    public class AddQualificationViewModel
    {
        public Types QualificationType { get; set; }
        public string? QualificationName { get; set; }
        public string? InstitutionName { get; set; }
        public DateTime? YearObtained { get; set; }
        public int? SkillId { get; set; }
        public int? ProficiencyId { get; set; }
    }
}
