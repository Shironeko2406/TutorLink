using Microsoft.AspNetCore.Mvc;
using TutorLinkAPI.BusinessLogics.IServices;
using TutorLinkAPI.ViewModel;
using System;
using System.Threading.Tasks;

namespace TutorLinkAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApplyController : Controller
    {
        private readonly IApplyService _applyService;

        public ApplyController(IApplyService applyService)
        {
            _applyService = applyService;
        }

        #region Get All Applies
        [HttpGet]
        [Route("GetAllApplies")]
        public async Task<IActionResult> GetAllApplies()
        {
            var applyList = await _applyService.GetAllApplies();
            if (applyList == null)
            {
                return BadRequest("Failed to retrieve apply list.");
            }
            return Ok(applyList);
        }
        #endregion

        #region Get Apply By Id
        [HttpGet("{applyId}")]
        public async Task<IActionResult> GetApplyById(Guid applyId)
        {
            var apply = await _applyService.GetApplyById(applyId);
            if (apply == null)
            {
                return NotFound("Apply not found.");
            }
            return Ok(apply);
        }
        #endregion

        #region Add New Apply
        [HttpPost]
        [Route("AddNewApply")]
        public async Task<IActionResult> AddNewApply([FromBody] AddApplyViewModel applyViewModel)
        {
            var newApply = await _applyService.AddNewApply(applyViewModel);
            if (newApply == null)
            {
                return BadRequest("Failed to add new apply!");
            }
            return Ok("Add new apply success!");
        }
        #endregion

        #region Update Apply
        [HttpPut("{applyId}")]
        public async Task<IActionResult> UpdateApply(Guid applyId, [FromBody] UpdateApplyViewModel applyViewModel)
        {
            var updatedApply = await _applyService.UpdateApply(applyId, applyViewModel);
            if (updatedApply == null)
            {
                return BadRequest("Failed to update apply!");
            }
            return Ok("Updated apply successfully!");
        }
        #endregion

        #region Delete Apply
        [HttpDelete("{applyId}")]
        public async Task<IActionResult> DeleteApply(Guid applyId)
        {
            var result = await _applyService.DeleteApply(applyId);
            if (!result)
            {
                return BadRequest("Failed to delete apply!");
            }
            return Ok("Deleted apply successfully!");
        }
        #endregion
    }
}
