using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers;

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{
    [HttpGet("open")]
    public IActionResult Open() => Ok("Open endpoint");

    [Authorize]
    [HttpGet("secure")]
    public IActionResult Secure() => Ok("Secured endpoint");
    
    [Authorize(Roles = "Admin")]
    [HttpGet("admin")]
    public IActionResult Admin() => Ok("Admin endpoint");
    
    [Authorize(Roles = "Member")]
    [HttpGet("member")]
    public IActionResult Member() => Ok("Member endpoint");
}