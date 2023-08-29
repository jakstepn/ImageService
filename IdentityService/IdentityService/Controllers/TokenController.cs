using IdentityService.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers;

[Route("")]
[ApiController]
public class TokenController : ControllerBase
{
    [HttpGet("token")]
    public ActionResult<Token> GetToken([FromBody] User user)
    {
        var result = new Token
        {
            Value = "TestToken",
            Type = "Bearer"
        };
        return Ok(result);
    }
}