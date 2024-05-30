using Microsoft.AspNetCore.Mvc;
using DataLayer.Entities;
using TutorLinkAPI.BusinessLogics.IServices;
using System.Collections.Generic;

namespace TutorLinkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplyController : ControllerBase
    {
        private readonly IApplyService _applyService;

        public ApplyController(IApplyService repository)
        {
            _applyService = repository;
        }

        // GET: api/apply
        [HttpGet]
        public ActionResult<IEnumerable<Apply>> GetAllApplies()
        {
            var applies = _applyService.GetAllApplies();
            return Ok(applies);
        }

        // GET: api/apply/{id}
        [HttpGet("{id}")]
        public ActionResult<Apply> GetApplyById(Guid id)
        {
            var apply = _applyService.GetApplyById(id);
            if (apply == null)
            {
                return NotFound();
            }
            return Ok(apply);
        }

        // POST: api/apply
        [HttpPost]
        public ActionResult<Apply> AddApply(Apply apply)
        {
            _applyService.CreateApply(apply);

            return CreatedAtAction(nameof(GetApplyById), new { id = apply.ApplyId }, apply);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateApply(Guid id,Apply apply)
        {
            if (id != apply.ApplyId)
            {
                return BadRequest();
            }

            var existingApply = _applyService.GetApplyById(id);
            if (existingApply == null)
            {
                return NotFound();
            }

            _applyService.UpdateApply(apply);

            return NoContent();
        }

        // DELETE: api/apply/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteApply(Guid id)
        {
            var apply = _applyService.GetApplyById(id);
            if (apply == null)
            {
                return NotFound();
            }

            _applyService.DeleteApply(id);
            return NoContent();
        }
    }
}
