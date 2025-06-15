using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UserManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DummyController: ControllerBase
{
    [HttpGet]
    [Authorize("Read")]
    public Task<ActionResult> Get()
    {
        return Task.FromResult<ActionResult>(Ok("Hello World"));
    }
    
    [HttpPost]
    [Authorize("Write")]
    public Task<ActionResult> Post()
    {
        return Task.FromResult<ActionResult>(Ok("Hello World"));
    }
}
