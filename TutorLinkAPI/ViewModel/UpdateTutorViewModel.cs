using DataLayer.Entities;

namespace TutorLinkAPI.ViewModel
{
    public class UpdateTutorViewModel
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string? AvatarUrl { get; set; }
        public TutorGenders Gender { get; set; }
    }
}
