using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TutorLinkAPI.BusinessLogics.IServices;
using TutorLinkAPI.ViewModel;

namespace TutorLinkAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class PostRequestController : ControllerBase
{
    private readonly IPostRequestService _postRequestService;

    public PostRequestController(IPostRequestService postRequestService)
    {
        _postRequestService = postRequestService;
    }
    
    #region Get All Post Requests
    [HttpGet]
    [Route("post-requests")]
    public async Task<IActionResult> GetAllPostRequests()
    {
        var postRequests = await _postRequestService.GetAllPostRequests();
        if (postRequests == null)
        {
            return BadRequest("Failed to retrieve post requests list.");
        }

        return Ok(postRequests);
    }
    #endregion

    #region Get All Pending Post Requests
    [HttpGet]
    [Authorize(Policy = "AdminOrStaff")]
    [Route("pending-post-requests")]
    public async Task<IActionResult> GetAllPendingPostRequests()
    {
        var postRequests = await _postRequestService.GetAllPendingPostRequests();
        if (postRequests == null)
        {
            return BadRequest("Failed to retrieve pending post requests list.");
        }

        return Ok(postRequests);
    }
    #endregion

    #region Get Post Request By Id
    [HttpGet]
    [Route("post-request-id/{id}")]
    public async Task<IActionResult> GetPostRequestById(Guid id)
    {
        var postRequests = await _postRequestService.GetPostRequestById(id);
        if (postRequests == null)
        {
            return BadRequest("Failed to retrieve post request");
        }

        return Ok(postRequests);
    }
    #endregion

    #region Get Post Request By UserId
    [HttpGet]
    [Route("post-request-user/{id}")]
    public async Task<IActionResult> GetPostRequestByUserId(Guid id)
    {
        var postRequest = await _postRequestService.GetPostRequestByUserId(id);
        if (postRequest == null)
        {
            return BadRequest("Failed to get post request of user");
        }
    
        return Ok(postRequest);
    }
    #endregion

    #region Get Post Request By User Login
    [HttpGet]
    [Route("post-request-user-login")]
    [Authorize]
    public async Task<IActionResult> GetPostRequestByUserLogin()
    {
        var postRequest = await _postRequestService.GetPostRequestByUserLogin(User);
        if (postRequest == null)
        {
            return BadRequest("Failed to get post request of user");
        }

        return Ok(postRequest);
    }
    #endregion

    #region Add New Post Request
    [HttpPost]
    [Route("add-post-request")]
    public async Task<IActionResult> AddNewPostRequest(AddPostRequestViewModel newPost)
    {
        var addedPost = await _postRequestService.AddNewPostRequest(newPost, User);
        if (addedPost == null)
        {
            return BadRequest("Failed to add new post request"); 
        }
        return Ok("Add new post request success!"); 
    }
    #endregion
    
    #region Update Post Request
    [HttpPut]
    [Route("update-post-request/{id}")]
    public async Task<IActionResult> UpdatePostRequest(Guid id, AddPostRequestViewModel newPost)
    {
        var updatePost = await _postRequestService.UpdatePostRequest(id, newPost, User);
        if (updatePost == null)
        {
            return BadRequest("Failed to update a post request");
        }
    
        return Ok("Updated post request success");
    }
    #endregion

    #region Update Post Request Status
    [Authorize(Policy = "AdminOrStaff")]
    [HttpPut]
    [Route("update-post-request-status/{id}")]
    public async Task<IActionResult> UpdatePostRequestStatus(Guid id, [FromBody] RequestStatuses newStatus)
    {
        var result = await _postRequestService.UpdatePostRequestStatus(id, newStatus, User);

        if (!result.Success)
        {
            return BadRequest(new { Error = result.ErrorMessage });
        }

        return Ok("Post request status updated successfully.");
    }
    #endregion

    #region Delete Post Request By PostId
    [HttpDelete]
    [Route("post-request-postId/{id}")]
    public async Task<IActionResult> DeletePostRequest(Guid id)
    {
        await _postRequestService.DeletePostRequest(id, User);
        return Ok("Successfully deleted post request");
    }
    #endregion
}