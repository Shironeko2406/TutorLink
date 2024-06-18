using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TutorLinkAPI.BusinessLogics.IServices;
using TutorLinkAPI.ViewModel;

namespace TutorLinkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplyController : ControllerBase
    {
        private readonly IApplyService _applyService;

        public ApplyController(IApplyService applyService)
        {
            _applyService = applyService;
        }

        [HttpPost("{tutorId}/{postId}")]
        public async Task<IActionResult> AddApply(Guid tutorId, Guid postId, [FromBody] AddApplyViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var apply = await _applyService.AddNewApply(tutorId, postId, model);
                return Ok(apply);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{applyId}")]
        public IActionResult GetApplyById(Guid applyId)
        {
            try
            {
                var apply = _applyService.GetApplyById(applyId);
                if (apply == null)
                    return NotFound("Apply not found.");

                return Ok(apply);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{applyId}")]
        public IActionResult UpdateApplyStatus(Guid applyId, [FromBody] UpdateApplyViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _applyService.UpdateApplyStatus(applyId, model);
                return Ok("Apply status updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{applyId}")]
        public IActionResult DeleteApply(Guid applyId)
        {
            try
            {
                _applyService.DeleteApply(applyId);
                return Ok("Apply deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("tutor/{tutorId}")]
        public IActionResult GetAppliesByTutorId(Guid tutorId)
        {
            try
            {
                var applies = _applyService.GetAppliesByTutorId(tutorId);
                return Ok(applies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAllApplies()
        {
            try
            {
                var applies = _applyService.GetAllApplies();
                return Ok(applies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
