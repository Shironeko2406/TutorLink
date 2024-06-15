using Microsoft.AspNetCore.Mvc;
using TutorLinkAPI.BusinessLogics.IServices;
using TutorLinkAPI.ViewModel;

namespace TutorLinkAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class QualificationController : ControllerBase
{
    private readonly IQualificationService _qualificationService;

    public QualificationController(IQualificationService qualificationService)
    {
        _qualificationService = qualificationService;
    }

    #region Get All Qualifications
    [HttpGet]
    [Route("qualifications")]
    public async Task<IActionResult> GetAllQualifications()
    {
        var qualificationList= await _qualificationService.GetAllQualifications();
        if (qualificationList == null)
        {
            return BadRequest("Failed to retrieve qualification list.");
        }
        return Ok(qualificationList);
    }
    #endregion

    #region Add New Qualification
    [HttpPost]
    [Route("add-qualification-for-tutor")]
    public async Task<IActionResult> AddNewQualification(Guid tutorId, AddQualificationViewModel qualificationViewModel)
    {
        var newQualification = await _qualificationService.AddNewQualification(tutorId, qualificationViewModel);
        if (newQualification == null)
        {
            return BadRequest("Failed to add new qualification!");
        }
        return Ok("Add new qualification success!");
    }
    #endregion

    #region Update Qualification By TutorId, QualificationId
    [HttpPut]
    [Route("update-qualification")]
    public async Task<IActionResult> UpdateQualification([FromHeader] Guid tutorId,
                                                         [FromHeader] Guid qualificationId,
                                                         [FromBody] UpdateQualificationViewModel qualificationViewModel)
    {
        var updatedQualification = await _qualificationService.UpdateQualification(tutorId, qualificationId, qualificationViewModel);
        if (updatedQualification == null)
        {
            return BadRequest("Failed to add new qualification!");
        }
        return Ok("Updated Qualification successfully!");
    }
    #endregion
    
    #region Delete Qualification By TutorId, QualificationId
    [HttpDelete]
    [Route("delete-qualification")]
    public async Task<IActionResult> DeleteQualification([FromHeader] Guid tutorId, [FromHeader] Guid qualificationId)
    {
        return Ok("Delete Qualification successfully!");
    }
    #endregion
}