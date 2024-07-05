using DataLayer.Entities;

namespace TutorLinkAPI.ViewModel;

public class PostRequestViewModel
{
    public Guid PostId { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public string Schedule { get; set; }
    public string PreferredTime { get; set; }
    public RequestMode Mode { get; set; }
    public RequestGender Gender { get; set; }
    public RequestStatuses Status { get; set; }
    public string RequestSkill { get; set; }
    public Guid CreatedBy { get; set; }
    public string CreatedByUsername { get; set; }
    public string? AvatarUrl { get; set; }
    public DateTime CreatedDate { get; set; }
    
    public virtual ICollection<ApplyViewModel>? Applies { get; set; }
}