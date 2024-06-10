namespace DataLayer.Entities;

public class Account
{
    public Guid AccountId { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Fullname { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public UserGenders Gender { get; set; }
    public int RoleId { get; set; }
    
    public virtual ICollection<PostRequest>? PostRequests { get; set; }   
    public virtual Role? Role { get; set; }
}

public enum UserGenders
{
    Female, 
    Male,
    Other
}