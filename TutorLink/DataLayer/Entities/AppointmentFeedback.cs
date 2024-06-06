using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class AppointmentFeedback
    {
        public int AppointmentId { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public DateTime? AppointmentTime { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public bool? IsSuccessful { get; set; }
        public AppStatuses Status { get; set; }
        public Guid? AccountId { get; set; }
        public Guid? TutorId { get; set; }
        public Guid? PostId { get; set; }
        
        public virtual Account? Account { get; set; }
        public virtual Tutor? Tutor { get; set; }
        public virtual PostRequest? PostRequest { get; set; }
    }
}
public enum AppStatuses
{
    Pending,
    Scheduled,
    Completed,
    Cancelled
}
