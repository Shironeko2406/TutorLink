using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DataLayer.Entities;
using DataLayer.DAL.Repositories;
using AutoMapper;
using TutorLinkAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PostRequestController : ControllerBase
{
    private readonly GenericRepository<PostRequest> _repository;
    private readonly ILogger<PostRequestController> _logger;
    private readonly IMapper _mapper;

    public PostRequestController(GenericRepository<PostRequest> repository, ILogger<PostRequestController> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    // GET: api/postrequest
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostRequestViewModel>>> GetAllPostRequests()
    {
        try
        {
            var postRequests = await _repository.GetAllWithAsync();
            var postRequestViewModels = _mapper.Map<IEnumerable<PostRequestViewModel>>(postRequests);
            return Ok(postRequestViewModels);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all post requests");
            return StatusCode(500, "Internal server error");
        }
    }

    // GET: api/postrequest/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<PostRequestViewModel>> GetPostRequestById(Guid id)
    {
        try
        {
            var postRequest = await _repository.GetSingleWithAsync(pr => pr.PostId == id);
            if (postRequest == null)
            {
                return NotFound();
            }
            var postRequestViewModel = _mapper.Map<PostRequestViewModel>(postRequest);
            return Ok(postRequestViewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting post request by id: {id}");
            return StatusCode(500, "Internal server error");
        }
    }

    // POST: api/postrequest
    [HttpPost]
    public async Task<ActionResult<PostRequestViewModel>> AddPostRequest(PostRequestViewModel postRequestViewModel)
    {
        try
        {
            var postRequest = _mapper.Map<PostRequest>(postRequestViewModel);
            postRequest.PostId = Guid.NewGuid(); // Generate a new GUID for PostId
            postRequest.CreatedDate = DateTime.UtcNow; // Set the created date

            await _repository.AddSingleWithAsync(postRequest);
            await _repository.SaveChangesAsync();

            var createdPostRequestViewModel = _mapper.Map<PostRequestViewModel>(postRequest);
            return CreatedAtAction(nameof(GetPostRequestById), new { id = createdPostRequestViewModel.PostId }, createdPostRequestViewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding post request");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // PUT: api/postrequest/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePostRequest(Guid id, PostRequestViewModel postRequestViewModel)
    {
        if (id != postRequestViewModel.PostId)
        {
            return BadRequest();
        }

        try
        {
            var existingPostRequest = await _repository.GetSingleWithAsync(pr => pr.PostId == id);
            if (existingPostRequest == null)
            {
                return NotFound();
            }

            var postRequest = _mapper.Map(postRequestViewModel, existingPostRequest);
            await _repository.UpdateWithAsync(postRequest);
            await _repository.SaveChangesAsync();

            var updatedPostRequestViewModel = _mapper.Map<PostRequestViewModel>(postRequest);
            return Ok(updatedPostRequestViewModel);  // Return the updated post request
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating post request with id: {id}");
            return StatusCode(500, "Internal server error");
        }
    }

    // DELETE: api/postrequest/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePostRequest(Guid id)
    {
        try
        {
            var postRequest = await _repository.GetSingleWithAsync(pr => pr.PostId == id);
            if (postRequest == null)
            {
                return NotFound();
            }

            _repository.Remove(postRequest);
            await _repository.SaveChangesAsync();

            return Ok(new { message = "Post request deleted successfully" });  // Return a success message
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting post request with id: {id}");
            return StatusCode(500, "Internal server error");
        }
    }
}
