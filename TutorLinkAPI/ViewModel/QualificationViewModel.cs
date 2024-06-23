using DataLayer.Entities;

namespace TutorLinkAPI.ViewModel
{
    public class QualificationViewModel
    {
        public Guid QualificationId { get; set; }
        public Types QualificationType { get; set; }
        public string? QualificationName { get; set; }
        public string? InstitutionName { get; set; }
        public DateTime? YearObtained { get; set; }
        public int? SkillId { get; set; }
        public int? ProficiencyId { get; set; }
        public Guid TutorId { get; set; }
    }
}
