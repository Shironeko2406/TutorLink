using Microsoft.AspNetCore.Mvc;
using TutorLinkAPI.BusinessLogics.IServices;
using TutorLinkAPI.ViewModel;

namespace TutorLinkAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class TutorController : ControllerBase
{
    private readonly ITutorService _tutorService;
    public TutorController(ITutorService tutorService)
    {
        _tutorService = tutorService;
    }

    #region Get Tutor List
    [HttpGet]
    [Route("tutors")]
    public async Task<IActionResult> GetTutorList()
    {
       var tutorList= await _tutorService.GetTutorList();
        if (tutorList == null)
        {
            return BadRequest("Failed to retrieve tutor list."); 
        }
        return Ok(tutorList);
    }
    #endregion

    #region Get Tutor By Id
        [HttpGet]
        [Route("tutor-by-id/{id}")]
        public async Task<IActionResult> GetTutorById(Guid id)
        {
        var tutor = await _tutorService.GetTutorById(id);
        if (tutor == null)
        {
            return BadRequest("Failed to retrieve tutor with this Id.");
        }
        return Ok(tutor);
    }
    #endregion

    #region Add New Tutor
    [HttpPost]
    [Route("add-tutor")]
    public async Task<IActionResult> AddNewTutor(AddTutorViewModel newTutor)
    {
        var tutor = await _tutorService.AddNewTutor(newTutor);
        if (tutor == null)
        {
            return BadRequest("Failed to add new tutor.");
        }
        return Ok(tutor!);
    }
    #endregion

    #region Update Tutor By Id
    [HttpPut]
    [Route("update-tutor/{id}")]
    public async Task<IActionResult> UpdateTutorById(Guid id, UpdateTutorViewModel updateTutor)
    {
        var tutor = await _tutorService.UpdateTutorById(id, updateTutor);
        if (tutor == null)
        {
            return BadRequest("Failed to add new tutor.");
        }
        return Ok(tutor);
    }
    #endregion
    
    #region Delete Tutor By Id
    [HttpDelete]
    [Route("delete-tutor/{id}")]
    public async Task<IActionResult> DeleteTutorById(Guid id)
    {
        return Ok("Deleted tutor successfully!");
    }
    #endregion
}