using DataLayer.Interfaces;

namespace DataLayer.Entities;

public class Account : IUser
{
    public Guid AccountId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Fullname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string? AvatarUrl { get; set; }
    public UserGenders Gender { get; set; }
    public int RoleId { get; set; }
    
    public virtual ICollection<PostRequest>? PostRequests { get; set; }   
    public virtual ICollection<AppointmentFeedback>? AppointmentFeedbacks { get; set; }
    public virtual Role? Role { get; set; }

    Guid IUser.UserId => AccountId;
}

public enum UserGenders
{
    Female, 
    Male,
    Other
}