using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using TutorLinkAPI.BusinessLogics.IServices;
using System;
using System.Threading.Tasks;

namespace TutorLinkAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApplyController : ControllerBase
    {
        private readonly IApplyService _applyService;
        private readonly ILogger<ApplyController> _logger;

        public ApplyController(IApplyService applyService, ILogger<ApplyController> logger)
        {
            _applyService = applyService;
            _logger = logger;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddNewApply([FromBody] ApplyRequestModel model)
        {
            try
            {
                await _applyService.AddNewApply(
                    model.TutorId,
                    model.PostRequestId,
                    model.Status
                );
                return Ok("Apply created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create apply");
                return BadRequest($"Failed to create apply: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        [HttpGet("list")]
        public async Task<IActionResult> ShowApplyList()
        {
            var applies = await _applyService.GetAllApplies();
            return Ok(applies);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetApplyById(Guid id)
        {
            var apply = await _applyService.GetApplyById(id);
            if (apply == null)
                return NotFound("Apply not found.");

            return Ok(apply);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateApply(Guid id, [FromBody] ApplyUpdateModel model)
        {
            try
            {
                await _applyService.UpdateApply(id, model.Status);
                return Ok("Apply updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update apply");
                return BadRequest($"Failed to update apply: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteApply(Guid id)
        {
            try
            {
                await _applyService.DeleteApply(id);
                return Ok("Apply deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete apply");
                return BadRequest($"Failed to delete apply: {ex.InnerException?.Message ?? ex.Message}");
            }
        }
    }

    public class ApplyRequestModel
    {
        public Guid TutorId { get; set; }
        public Guid PostRequestId { get; set; }
        public ApplyStatuses Status { get; set; }
    }

    public class ApplyUpdateModel
    {
        public ApplyStatuses Status { get; set; }
    }
}
