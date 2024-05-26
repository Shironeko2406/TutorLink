using Microsoft.AspNetCore.Mvc;

namespace TutorLinkAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class WalletController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}