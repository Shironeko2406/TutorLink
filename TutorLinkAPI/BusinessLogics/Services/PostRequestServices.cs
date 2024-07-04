using System.Security.Claims;
using AutoMapper;
using DataLayer.DAL.Repositories;
using DataLayer.Entities;
using TutorLinkAPI.BusinessLogics.IServices;
using TutorLinkAPI.ViewModel;

namespace TutorLinkAPI.BusinessLogics.Services;

public class PostRequestServices : IPostRequestService
{
    private readonly PostRequestRepository _postRequestRepository;
    private readonly IMapper _mapper;

    public PostRequestServices(PostRequestRepository postRequestRepository, IMapper mapper)
    {
        _postRequestRepository = postRequestRepository;
        _mapper = mapper;
    }
    public async Task<List<PostRequestViewModel>> GetAllPostRequests()
    {
        try
        {
            var postsRequests = await _postRequestRepository.GetAllWithAsync();
            var activePostRequests = postsRequests.Where(pr => pr.Status != RequestStatuses.Unactived);
            var postRequestViewModel = _mapper.Map<List<PostRequestViewModel>>(activePostRequests);
            return postRequestViewModel;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while getting all post requests.", ex);
        }
    }

    public async Task<AddPostRequestViewModel> AddNewPostRequest(AddPostRequestViewModel newPost, ClaimsPrincipal user)
    {
        try
        {
            var userIdClaim = user.FindFirst("UserId");
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                throw new ArgumentException("User ID claim not found or invalid.");
            }

            var fullnameClaim = user.FindFirst("Fullname");
            if (fullnameClaim == null)
            {
                throw new ArgumentException("Fullname claim not found.");
            }
            var newPostRequest = _mapper.Map<PostRequest>(newPost);
            newPostRequest.PostId = Guid.NewGuid();
            newPostRequest.CreatedBy = userId;
            newPostRequest.CreatedByUsername = fullnameClaim.Value;
            
            await _postRequestRepository.AddSingleWithAsync(newPostRequest);
            await _postRequestRepository.SaveChangesAsync();

            return _mapper.Map<AddPostRequestViewModel>(newPostRequest);
        }
        catch (Exception e)
        {
            throw new Exception("Error saving entity changes: " + e.Message);
        }
    }

    public async Task<AddPostRequestViewModel> UpdatePostRequest(Guid postId, AddPostRequestViewModel updatedPost, ClaimsPrincipal user)
    {
        try
        {
            var existedPostRequest = await _postRequestRepository.GetByIdAsync(postId);
            if (existedPostRequest == null)
            {
                throw new ArgumentException("PostRequest not found with this ID");
            }
    
            var userIdClaim = user.FindFirst("UserId");
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId) || existedPostRequest.CreatedBy != userId)
            {
                throw new UnauthorizedAccessException("User does not have permission to update this PostRequest.");
            }
    
            _mapper.Map(updatedPost, existedPostRequest);
            await _postRequestRepository.UpdateWithAsync(existedPostRequest);
            await _postRequestRepository.SaveChangesAsync();
    
            return _mapper.Map<AddPostRequestViewModel>(existedPostRequest);
        }
        catch (Exception e)
        {
            throw new Exception("Error updating PostRequest: " + e.Message);
        }
    }

    public async Task<List<PostRequestViewModel>> GetPostRequestByUserId(Guid userId)
    {
        try
        {
            var postRequests = await _postRequestRepository.GetAllWithAsync();
            var postRequestViewModel = _mapper.Map < List<PostRequestViewModel>>(postRequests);
            var userPostRequest = postRequestViewModel
                .Where(p => p.CreatedBy == userId)
                .ToList();
            return userPostRequest;
        }
        catch (Exception e)
        {
            throw new Exception("An error occurred while getting post requests for the user.", e);
        }
    }

    public async Task<List<PostRequestViewModel>> GetPostRequestByUserLogin(ClaimsPrincipal user)
    {
        try
        {
            var userIdClaim = user.FindFirst("UserId");
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                throw new ArgumentException("User ID claim not found or invalid.");
            }

            var postRequests= await _postRequestRepository.GetAllWithAsync();
            var postRequestViewModel= _mapper.Map<List<PostRequestViewModel>>(postRequests);
            var userPostRequest = postRequestViewModel.Where(p => p.CreatedBy == userId) .ToList();

            return userPostRequest;
        }
        catch (Exception e)
        {

            throw new Exception("An error occurred while getting post requests for the user.", e);
        }
    }

    public async Task<PostRequestViewModel> GetPostRequestById(Guid id)
    {
        try
        {
            var postRequest = await _postRequestRepository.GetByIdAsync(id);
            if (postRequest == null)
            {
                throw new Exception($"Post request with ID {id} not found.");
            }
            var postRequestModel = _mapper.Map<PostRequestViewModel>(postRequest);
           
            return postRequestModel;
        }
        catch (Exception e)
        {

            throw new Exception("An error occurred while getting post requests.", e);
        }
    }

    public async Task DeletePostRequest(Guid id, ClaimsPrincipal user)
    {
        try
        {
            var existedPostRequest = await _postRequestRepository.GetByIdAsync(id);
            if (existedPostRequest == null)
            {
                throw new ArgumentException("PostRequest not found with this ID");
            }

            var userIdClaim = user.FindFirst("UserId");
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId) || existedPostRequest.CreatedBy != userId)
            {
                throw new UnauthorizedAccessException("User does not have permission to delete this PostRequest.");
            }

            existedPostRequest.Status = RequestStatuses.Unactived;
            await _postRequestRepository.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception("Error deleting PostRequest: " + e.Message);
        }
    }

}