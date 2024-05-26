namespace DataLayer.Entities;

public class Appointment
{
    public int AppointmentId { get; set; }
    public DateTime ExpiredDate { get; set; }
    public DateTime AppTime { get; set; }
    public string Address { get; set; }
    public AppStatuses Status { get; set; }
    public Guid ParentId { get; set; }
    public Guid TutorId { get; set; }
    public Guid PostId { get; set; }
    
    public virtual PostRequest? PostRequest { get; set; }
    public virtual ICollection<ParentFeedback> ParentFeedbacks { get; set; }
}
public enum AppStatuses
{
    Pending,
    Scheduled,
    Completed,
    Cancelled
}