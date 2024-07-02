using DataLayer.Entities;

namespace TutorLinkAPI.ViewModel
{
    public class TutorViewModel
    {
        public Guid TutorId { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string? AvatarUrl { get; set; }
        public TutorGenders Gender { get; set; }

        public List<QualificationViewModel>? Qualifications { get; set; }
    }
}
