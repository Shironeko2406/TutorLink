namespace DataLayer.Entities;

public class Role
{
    public int RoleId { get; set; }
    public string? RoleName { get; set; }
    
    public virtual ICollection<Account>? Accounts { get; set; }
    public virtual ICollection<Tutor>? Tutors { get; set; }
}
