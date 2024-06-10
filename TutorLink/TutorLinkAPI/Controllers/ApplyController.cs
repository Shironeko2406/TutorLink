using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DataLayer.Entities;
using DataLayer.DAL.Repositories;
using AutoMapper;
using TutorLinkAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ApplyController : ControllerBase
{
    private readonly ApplyRepository _repository;
    private readonly ILogger<ApplyController> _logger;
    private readonly IMapper _mapper;

    public ApplyController(ApplyRepository repository, ILogger<ApplyController> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    // GET: api/apply
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ApplyViewModel>>> GetAllApplies()
    {
        try
        {
            var applies = await _repository.GetAllWithAsync();
            var applyViewModels = _mapper.Map<IEnumerable<ApplyViewModel>>(applies);
            return Ok(applyViewModels);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all applies");
            return StatusCode(500, "Internal server error");
        }
    }

    // GET: api/apply/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ApplyViewModel>> GetApplyById(Guid id)
    {
        try
        {
            var apply = await _repository.GetByIdAsync(id);
            if (apply == null)
            {
                return NotFound();
            }
            var applyViewModel = _mapper.Map<ApplyViewModel>(apply);
            return Ok(applyViewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting apply by id: {id}");
            return StatusCode(500, "Internal server error");
        }
    }

    // POST: api/apply
    [HttpPost]
    public async Task<ActionResult<ApplyViewModel>> AddApply(ApplyViewModel applyViewModel)
    {
        try
        {
            var apply = _mapper.Map<Apply>(applyViewModel);
            apply.ApplyId = Guid.NewGuid(); // Generate a new GUID for ApplyId

            await _repository.AddSingleWithAsync(apply);
            await _repository.SaveChangesAsync();

            var createdApplyViewModel = _mapper.Map<ApplyViewModel>(apply);
            return CreatedAtAction(nameof(GetApplyById), new { id = createdApplyViewModel.ApplyId }, createdApplyViewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding apply");
            return StatusCode(500, "Internal server error");
        }
    }

    // PUT: api/apply/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateApply(Guid id, ApplyViewModel applyViewModel)
    {
        if (id != applyViewModel.ApplyId)
        {
            return BadRequest();
        }

        try
        {
            var existingApply = await _repository.GetByIdAsync(id);
            if (existingApply == null)
            {
                return NotFound();
            }

            var apply = _mapper.Map(applyViewModel, existingApply);
            await _repository.UpdateWithAsync(apply);
            await _repository.SaveChangesAsync();

            var updatedApplyViewModel = _mapper.Map<ApplyViewModel>(apply);
            return Ok(updatedApplyViewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating apply with id: {id}");
            return StatusCode(500, "Internal server error");
        }
    }

    // DELETE: api/apply/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteApply(Guid id)
    {
        try
        {
            var apply = await _repository.GetByIdAsync(id);
            if (apply == null)
            {
                return NotFound();
            }

            _repository.Remove(apply);
            await _repository.SaveChangesAsync();

            return Ok(new { message = "Apply deleted successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting apply with id: {id}");
            return StatusCode(500, "Internal server error");
        }
    }
}
