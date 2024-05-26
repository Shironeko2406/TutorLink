namespace DataLayer.Entities;

public class ParentFeedback
{
    public int FeedbackId { get; set; }
    public string Description { get; set; }
    public bool IsSucessful { get; set; }
    public int AppointmentId { get; set; }
    
    public virtual Appointment? Appointment { get; set; }
}