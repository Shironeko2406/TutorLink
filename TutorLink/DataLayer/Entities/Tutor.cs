using DataLayer.Interfaces;

namespace DataLayer.Entities;

public class Tutor : IUser
{
    public Guid TutorId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Fullname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string? AvatarUrl { get; set; }
    public TutorGenders Gender { get; set; }
    public int RoleId { get; set; }
    
    public virtual Role? Role { get; set; }
    public virtual Wallet? Wallet { get; set; }
    public virtual ICollection<Apply>? Applies { get; set; }
    public virtual ICollection<Qualification>? Qualifications { get; set; }
    public virtual ICollection<AppointmentFeedback>? AppointmentFeedbacks { get; set; }

    Guid IUser.UserId => TutorId;

}

public enum TutorGenders
{
    Female, 
    Male,
    Other
}