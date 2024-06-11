using DataLayer.Entities;
using TutorLinkAPI.ViewModel;

namespace TutorLinkAPI.BusinessLogics.IServices
{
    public interface IAuthService
    {
        AccessTokenViewModel GenerateToken(IUser user);
        AccessTokenViewModel Login(string username, string password);
    }
}
