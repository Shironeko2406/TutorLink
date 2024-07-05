using System.Security.Claims;
using AutoMapper;
using DataLayer.DAL.Repositories;
using DataLayer.Entities;
using TutorLinkAPI.BusinessLogics.IServices;
using TutorLinkAPI.ViewModel;
#pragma warning disable CS8603
namespace TutorLinkAPI.BusinessLogics.Services;

public class PostRequestServices : IPostRequestService
{
    private readonly PostRequestRepository _postRequestRepository;
    private readonly ApplyRepository _applyRepository;
    private readonly IMapper _mapper;

    public PostRequestServices(PostRequestRepository postRequestRepository, ApplyRepository applyRepository, IMapper mapper)
    {
        _postRequestRepository = postRequestRepository;
        _applyRepository = applyRepository;
        _mapper = mapper;
    }
    public async Task<List<PostRequestViewModel>> GetAllPostRequests()
    {
        try
        {
            var postsRequests = await _postRequestRepository.GetAllWithAsync();
            var activePostRequests = postsRequests.Where(pr => pr.Status != RequestStatuses.Unactived).ToList();
            var postRequestViewModels = _mapper.Map<List<PostRequestViewModel>>(activePostRequests);

            //foreach (var postRequestViewModel in postRequestViewModels)
            //{
            //    var applies = _applyRepository.GetAll()
            //        .Where(a => a.PostId == postRequestViewModel.PostId)
            //        .ToList();

            //    postRequestViewModel.Applies = _mapper.Map<List<ApplyViewModel>>(applies);
            //}

            foreach (var postRequestViewModel in postRequestViewModels)
            {
                var applies = await _applyRepository.GetAllWithIncludeAsync(
                    a => a.PostId == postRequestViewModel.PostId,
                    a => a.Tutor
                );

                var applyViewModels = _mapper.Map<List<ApplyViewModel>>(applies);
            }

            return postRequestViewModels;
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
            newPostRequest.Status= RequestStatuses.Pending;

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
            var postRequests = await _postRequestRepository.GetAllWithIncludeAsync(
                p => p.CreatedBy == userId,
                p => p.Applies
            );
            var postRequestViewModels = _mapper.Map<List<PostRequestViewModel>>(postRequests);
            foreach (var postRequestViewModel in postRequestViewModels)
            {
                var applies = await _applyRepository.GetAllWithIncludeAsync(
                    a => a.PostId == postRequestViewModel.PostId,
                    a => a.Tutor
                );

                postRequestViewModel.Applies = _mapper.Map<List<ApplyViewModel>>(applies);
            }

            return postRequestViewModels;
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

            var postRequests = await _postRequestRepository.GetAllWithIncludeAsync(
                p => p.CreatedBy == userId,
                p => p.Applies
            );
            var postRequestViewModels = _mapper.Map<List<PostRequestViewModel>>(postRequests);
            foreach (var postRequestViewModel in postRequestViewModels)
            {
                var applies = await _applyRepository.GetAllWithIncludeAsync(
                    a => a.PostId == postRequestViewModel.PostId,
                    a => a.Tutor
                );

                postRequestViewModel.Applies = _mapper.Map<List<ApplyViewModel>>(applies);
            }

            return postRequestViewModels;
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
            var applies = await _applyRepository.GetAllWithIncludeAsync(
                a => a.PostId == id,
                a => a.Tutor 
            );

            postRequestModel.Applies = _mapper.Map<List<ApplyViewModel>>(applies);

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

    public async Task<OperationResult> UpdatePostRequestStatus(Guid postId, RequestStatuses newStatus, ClaimsPrincipal user)
    {
        var roleClaim = user.FindFirst("Role");
        if (roleClaim == null || (roleClaim.Value != "1" && roleClaim.Value != "2"))
        {
            return new OperationResult { Success = false, ErrorMessage = "User does not have permission to update post requests." };
        }

        var postRequest = await _postRequestRepository.GetByIdAsync(postId);
        if (postRequest == null)
        {
            return new OperationResult { Success = false, ErrorMessage = $"Post request with ID {postId} not found." };
        }

        postRequest.Status = newStatus;
        await _postRequestRepository.UpdateWithAsync(postRequest);

        return new OperationResult { Success = true };
    }

    public async Task<List<PostRequestViewModel>> GetAllPendingPostRequests()
    {
        var postRequests = await _postRequestRepository.GetAllWithAsync();
        var pendingPostRequest = postRequests.Where(p => p.Status == RequestStatuses.Pending);
        var postRequestViewModels = _mapper.Map<List<PostRequestViewModel>>(pendingPostRequest);

        return postRequestViewModels;
    }

    public class OperationResult
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
    }
}