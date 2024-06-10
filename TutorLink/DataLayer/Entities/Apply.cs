using System;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
    public class Apply
    {
        [Key]
        public Guid ApplyId { get; set; }
        [Required]
        public Guid PostId { get; set; }
        [Required]
        public Guid TutorId { get; set; }
        [Required]
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
}
