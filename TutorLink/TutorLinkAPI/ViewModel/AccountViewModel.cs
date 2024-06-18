using DataLayer.Entities;

namespace TutorLinkAPI.ViewModel;

public class AccountViewModel
{
    public Guid AccountId { get; set; }
    public string Username { get; set; }
    public string Fullname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public UserGenders Gender { get; set; }
    public virtual ICollection<PostRequest>? PostRequests { get; set; }

}
