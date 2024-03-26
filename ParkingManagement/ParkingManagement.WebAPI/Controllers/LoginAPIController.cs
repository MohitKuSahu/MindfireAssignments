using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ParkingManagement.Models;
using System;
using ParkingManagement.BL;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ParkingManagement.DAL;
using ParkingManagement.Utils;


[Route("api/[controller]")]
[ApiController]
public class LoginAPIController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IBL _userBAL;
    private readonly ILog _Log;

    public LoginAPIController(IConfiguration config, IBL userBAL, ILog Log)
    {
        _config = config;
        _userBAL = userBAL;
        _Log = Log; 
    }

    private async Task<UserModel> AuthenticateUserAsync(UserModel user)
    {
        int userId = await _userBAL.CheckIfUserExists(user);

        return userId != -1 ? user : null;
    }

    private string GenerateJwtToken(UserModel user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
      {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Role, user.Type)
        };

        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
             claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> LoginAsync([FromBody] UserModel user)
    {
        IActionResult response = Unauthorized();
        var authenticatedUser = await AuthenticateUserAsync(user);

        if (authenticatedUser != null)
        {
            var token = GenerateJwtToken(authenticatedUser);
            response = Ok(new { token });
        }

        return response;
    }
}
