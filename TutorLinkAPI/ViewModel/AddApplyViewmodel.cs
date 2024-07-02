using System;
using System.ComponentModel.DataAnnotations;
using DataLayer.Entities;

namespace TutorLinkAPI.ViewModel
{
    public class AddApplyViewModel
    {
        public Guid PostId { get; set; }
        public Guid TutorId { get; set; }

        [Required]
        public ApplyStatuses Status { get; set; }
    }
}
