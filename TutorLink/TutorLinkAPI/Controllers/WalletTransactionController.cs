using Microsoft.AspNetCore.Mvc;

namespace TutorLinkAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class WalletTransactionController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok();
    }
}