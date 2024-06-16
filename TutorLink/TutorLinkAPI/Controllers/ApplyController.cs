using System;
using Microsoft.AspNetCore.Mvc;
using TutorLinkAPI.BusinessLogics.IServices;
using TutorLinkAPI.ViewModel;

namespace TutorLinkAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApplyController : ControllerBase
    {
        private readonly IApplyService _applyService;

        public ApplyController(IApplyService applyService)
        {
            _applyService = applyService;
        }

        [HttpPost("add")]
        public IActionResult AddNewApply(Guid postId, Guid tutorId)
        {
            var newApply = _applyService.AddNewApply(postId, tutorId);

            var response = new
            {
                newApply.ApplyId,
                newApply.PostId,
                newApply.TutorId,
                newApply.Status
            };

            return Ok(response);
        }

        [HttpGet("get/{applyId}")]
        public IActionResult GetApplyById(Guid applyId)
        {
            var apply = _applyService.GetApplyById(applyId);
            if (apply == null)
                return NotFound("Apply not found.");

            var response = new
            {
                apply.ApplyId,
                apply.PostId,
                apply.TutorId,
                apply.Status
            };

            return Ok(response);
        }

        [HttpPut("update/{applyId}")]
        public IActionResult UpdateApplyStatus(Guid applyId, [FromBody] UpdateApplyViewModel model)
        {
            try
            {
                _applyService.UpdateApplyStatus(applyId, model);
                return Ok("Apply status updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to update apply status: {ex.Message}");
            }
        }

        [HttpDelete("delete/{applyId}")]
        public IActionResult DeleteApply(Guid applyId)
        {
            try
            {
                _applyService.DeleteApply(applyId);
                return Ok("Apply deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete apply: {ex.Message}");
            }
        }

        [HttpGet("tutor/{tutorId}")]
        public IActionResult GetAppliesByTutorId(Guid tutorId)
        {
            var applies = _applyService.GetAppliesByTutorId(tutorId);
            return Ok(applies);
        }

        [HttpGet("list")]
        public IActionResult GetAllApplies()
        {
            var applies = _applyService.GetAllApplies();
            return Ok(applies);
        }
    }
}
