using Microsoft.AspNetCore.Mvc;
using TutorLinkAPI.BusinessLogics.IServices;
using TutorLinkAPI.ViewModel;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TutorLinkAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApplyController : Controller
    {
        private readonly IApplyService _applyService;
        private readonly ILogger<ApplyController> _logger;

        public ApplyController(IApplyService applyService, ILogger<ApplyController> logger)
        {
            _applyService = applyService;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAllApplies")]
        public async Task<IActionResult> GetAllApplies()
        {
            try
            {
                var applyList = await _applyService.GetAllApplies();
                if (applyList == null)
                {
                    return BadRequest("Failed to retrieve apply list.");
                }
                return Ok(applyList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all applies.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet]
        [Route("GetApplyById")]
        public async Task<IActionResult> GetApplyById(Guid applyId)
        {
            try
            {
                var apply = await _applyService.GetApplyById(applyId);
                if (apply == null)
                {
                    return NotFound("Apply not found.");
                }
                return Ok(apply);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting apply by ID.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost]
        [Route("AddNewApply")]
        public async Task<IActionResult> AddNewApply([FromBody] AddApplyViewModel applyViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newApply = await _applyService.AddNewApply(applyViewModel);
                if (newApply == null)
                {
                    return BadRequest("Failed to add new apply!");
                }
                return Ok("Add new apply success!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding new apply.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateApply(Guid applyId, [FromBody] UpdateApplyViewModel applyViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedApply = await _applyService.UpdateApply(applyId, applyViewModel);
                if (updatedApply == null)
                {
                    return BadRequest("Failed to update apply!");
                }
                return Ok("Updated apply successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating apply.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete]
        [Route("DeleteApply")]
        public async Task<IActionResult> DeleteApply(Guid applyId)
        {
            try
            {
                var result = await _applyService.DeleteApply(applyId);
                if (!result)
                {
                    return BadRequest("Failed to delete apply!");
                }
                return Ok("Deleted apply successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting apply.");
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
