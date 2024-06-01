using Microsoft.AspNetCore.Mvc;
using TutorLinkAPI.BusinessLogics.IServices;
using TutorLinkAPI.ViewModel;

namespace TutorLinkAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class QualificationController : Controller
{
    private readonly IQualificationService _qualificationService;

    public QualificationController(IQualificationService qualificationService)
    {
        _qualificationService = qualificationService;
    }

    #region Get All Qualifications
    [HttpGet]
    [Route("GetAllQualifications")]
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
    [Route("AddNewQualification")]
    public async Task<IActionResult> AddNewQualification(Guid tutorId, QualificationViewModel qualificationViewModel)
    {
        qualificationViewModel.TutorId = tutorId;
        var newQualification = await _qualificationService.AddNewQualification(qualificationViewModel);
        if (newQualification == null)
        {
            return BadRequest("Failed to add new qualification!");
        }
        return Ok("Add new qualification success!");
    }
    #endregion
}