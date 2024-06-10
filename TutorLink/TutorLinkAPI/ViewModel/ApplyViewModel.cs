using DataLayer.Entities;
using System;

namespace TutorLinkAPI.ViewModels
{
    public class ApplyViewModel
    {
        public Guid ApplyId { get; set; }
        public Guid PostId { get; set; }
        public Guid TutorId { get; set; }
        public ApplyStatuses Status { get; set; }
    }
}
