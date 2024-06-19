using System.Security.Claims;
using TutorLinkAPI.ViewModel;

namespace TutorLinkAPI.BusinessLogics.IServices;

public interface IPostRequestService
{
    Task<List<PostRequestViewModel>> GetAllPostRequests();
    Task<AddPostRequestViewModel> AddNewPostRequest(AddPostRequestViewModel newPost, ClaimsPrincipal user);
    Task<AddPostRequestViewModel> UpdatePostRequest(Guid postId, AddPostRequestViewModel updatedPost, ClaimsPrincipal user);

    Task<List<PostRequestViewModel>> GetPostRequestByUserId(Guid userId);
    Task<List<PostRequestViewModel>> GetPostRequestByUserLogin(ClaimsPrincipal user);
    //Task DeletePostRequest(Guid id);
}