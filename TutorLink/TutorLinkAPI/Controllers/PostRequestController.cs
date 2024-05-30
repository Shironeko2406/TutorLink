using Microsoft.AspNetCore.Mvc;
using DataLayer.Entities;
using DataLayer.DAL.Repositories;
using System.Collections.Generic;
using TutorLinkAPI.BusinessLogics.IServices;
using TutorLinkAPI.BusinessLogics.Services;

namespace TutorLinkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostRequestController : ControllerBase
    {
        private readonly IPostRequestService _postRequestService;

        public PostRequestController(IPostRequestService repository)
        {
            _postRequestService = repository;
        }

        // GET: api/postrequest
        [HttpGet]
        public ActionResult<IEnumerable<PostRequest>> GetAllPostRequests()
        {
            var postRequests = _postRequestService.GetAllPostRequests();
            return Ok(postRequests);
        }

        // GET: api/postrequest/{id}
        [HttpGet("{id}")]
        public ActionResult<PostRequest> GetPostRequestById(Guid id)
        {
            var postRequest = _postRequestService.GetPostRequestById(id);
            if (postRequest == null)
            {
                return NotFound();
            }
            return Ok(postRequest);
        }

        // POST: api/postrequest
        [HttpPost]
        public ActionResult<PostRequest> AddPostRequest(PostRequest postRequest)
        {
            _postRequestService.CreatePostRequest(postRequest);
            return CreatedAtAction(nameof(GetPostRequestById), new { id = postRequest.PostId }, postRequest);
        }

        // PUT: api/postrequest/{id}
        [HttpPut("{id}")]
        public IActionResult UpdatePostRequest(Guid id, PostRequest postRequest)
        {
            if (id != postRequest.PostId)
            {
                return BadRequest();
            }

            var existingPostRequest = _postRequestService.GetPostRequestById(id);
            if (existingPostRequest == null)
            {
                return NotFound();
            }

            _postRequestService.UpdatePostRequest(postRequest);
            return NoContent();
        }

        // DELETE: api/postrequest/{id}
        [HttpDelete("{id}")]
        public IActionResult DeletePostRequest(Guid id)
        {
            var postRequest = _postRequestService.GetPostRequestById(id);
            if (postRequest == null)
            {
                return NotFound();
            }

            _postRequestService.DeletePostRequest(id);
            return NoContent();
        }
    }
}
