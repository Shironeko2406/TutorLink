using System;
using DataLayer.Entities;

namespace TutorLinkAPI.ViewModel
{
    public class ApplyViewModel
    {
        public Guid ApplyId { get; set; }
        public Guid PostId { get; set; }
        public Guid TutorId { get; set; }
        public ApplyStatuses Status { get; set; }
        public string Fullname { get; set; }
    }
}