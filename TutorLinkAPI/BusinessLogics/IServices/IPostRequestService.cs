using DataLayer.Entities;
using System.Security.Claims;
using TutorLinkAPI.ViewModel;
using static TutorLinkAPI.BusinessLogics.Services.PostRequestServices;

namespace TutorLinkAPI.BusinessLogics.IServices;

public interface IPostRequestService
{
    Task<List<PostRequestViewModel>> GetAllPostRequests();
    Task<List<PostRequestViewModel>> GetAllPendingPostRequests();
    Task<PostRequestViewModel> GetPostRequestById(Guid id);
    Task<AddPostRequestViewModel> AddNewPostRequest(AddPostRequestViewModel newPost, ClaimsPrincipal user);
    Task<AddPostRequestViewModel> UpdatePostRequest(Guid postId, AddPostRequestViewModel updatedPost, ClaimsPrincipal user);
    Task<OperationResult> UpdatePostRequestStatus(Guid postId, RequestStatuses newStatus, ClaimsPrincipal user);
    Task<List<PostRequestViewModel>> GetPostRequestByUserId(Guid userId);
    Task<List<PostRequestViewModel>> GetPostRequestByUserLogin(ClaimsPrincipal user);
    Task DeletePostRequest(Guid id, ClaimsPrincipal user);
}