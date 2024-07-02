namespace DataLayer.Entities;

public class Apply
{
    public Guid ApplyId { get; set; }
    public Guid PostId { get; set; }
    public Guid TutorId { get; set; }
    public ApplyStatuses Status { get; set; }

    public virtual Tutor? Tutor { get; set; }
    public virtual PostRequest? PostRequest { get; set; }
}
public enum ApplyStatuses
{
    Pending,
    Approved,
    Denied
}