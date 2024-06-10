namespace TutorLinkAPI.ViewModels
{
    public class AccountViewModel
    {
        public Guid AccountId { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Fullname { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public int RoleId { get; set; }

    }
}
