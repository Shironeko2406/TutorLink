namespace DataLayer.Entities;

public class Account
{
    public Guid AccountId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Fullname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
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