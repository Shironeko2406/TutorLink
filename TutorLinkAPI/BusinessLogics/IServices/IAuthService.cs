using DataLayer.Entities;
using DataLayer.Interfaces;
using System.Collections.Generic;
using TutorLinkAPI.ViewModel;

namespace TutorLinkAPI.BusinessLogics.IServices
{
    public interface IAuthService
    {
        AccessTokenViewModel GenerateToken(IUser user);
        AccessTokenViewModel Login(string username, string password);
        Task<AccessTokenViewModel> GoogleLogin(string idToken);
    }
}
