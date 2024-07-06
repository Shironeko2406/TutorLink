using DataLayer.Entities;

namespace TutorLinkAPI.ViewModel
{
    public class AccountGoogleViewModel
    {
        public Guid AccountId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public int RoleId { get; set; }
        public string Fullname { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string? AvatarUrl { get; set; }
        public UserGenders Gender { get; set; }
        public string AccessTokenToken { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
