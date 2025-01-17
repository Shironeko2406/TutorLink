using DataLayer.Entities;

namespace TutorLinkAPI.ViewModel;

public class AddPostRequestViewModel
{
    public string Description { get; set; }
    public string Location { get; set; }
    public string Schedule { get; set; }
    public string PreferredTime { get; set; }
    public RequestMode Mode { get; set; }
    public RequestGender Gender { get; set; }
    public RequestStatuses Status { get; set; }
    public string RequestSkill { get; set; }
    public DateTime CreatedDate { get; set; }
    public ICollection<Apply>? Applies { get; set; }
}