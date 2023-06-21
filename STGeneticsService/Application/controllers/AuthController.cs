using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace STGeneticsService.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController: ControllerBase
{
    private readonly ILogger<AnimalController> _logger;
    private readonly IConfiguration _configuration;

    private readonly IOauthService _oauthService;
    public AuthController(IConfiguration configuration, IOauthService oauthService, ILogger<AnimalController> logger)
    {
        _configuration = configuration;
        _oauthService = oauthService;
        _logger = logger;
    }

    [AllowAnonymous]
    [HttpPost]
    public IActionResult Auth([FromBody] UserDto user)
    {
        if (user == null)
        {
            return BadRequest("Invalid client request");
        }

        OauthResponse response = _oauthService.GetToken(user.userName, user.password); 
        if (response.code.Equals("401"))
        {
            return Unauthorized(response);
        }
        else
        {
            return Ok(response);
        }
       
    }

}