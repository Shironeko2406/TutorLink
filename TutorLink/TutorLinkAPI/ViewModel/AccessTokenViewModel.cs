namespace TutorLinkAPI.ViewModel
{
    public class AccessTokenViewModel
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string? AccessTokenToken { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
