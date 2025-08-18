using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiResponseExamplesDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private const string Username = "admin";
        private const string Password = "senha123";
        private const string JwtSecret = "super_secret_jwt_key_1234567890"; // Min 16 chars
        private const string Issuer = "ApiResponseExamplesDemo";
        private const string Audience = "ApiResponseExamplesDemoUsers";

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request.Username != Username || request.Password != Password)
            {
                return Unauthorized(new { message = "Usuário ou senha inválidos" });
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, request.Username),
                new Claim("role", "admin"), // claim fixa
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { token = tokenString });
        }

        [HttpGet("protected")]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public IActionResult Protected()
        {
            var userName = User.Identity?.Name ?? "desconhecido";
            return Ok(new { message = $"Olá, {userName}. Você acessou um endpoint protegido!" });
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
