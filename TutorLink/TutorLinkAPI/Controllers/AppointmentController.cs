using Microsoft.AspNetCore.Mvc;

namespace TutorLinkAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class AppointmentController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}