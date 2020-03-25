using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public IActionResult MakeSession([FromBody]AuthRequest request)
        {
            //just hard code here.  
            //if (name == "catcher" && pwd == "123")

            Console.WriteLine($"AUTH: name={request.Name} password={request.Password}");

            {
                var now = DateTime.UtcNow;

                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, request.Name),
                    new Claim(JwtRegisteredClaimNames.Sub, request.Name),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
                };

                var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("test secret 1234567890"));
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingKey,
                    ValidateIssuer = true,
                    ValidIssuer = "Swisschain",
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    RequireExpirationTime = true,

                };

                var jwt = new JwtSecurityToken(
                    issuer: "Swisschain",
                    claims: claims,
                    notBefore: now,
                    expires: now.Add(TimeSpan.FromMinutes(5000)),
                    signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                );
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                var responseJson = new
                {
                    access_token = encodedJwt,
                    expires_in = (int)TimeSpan.FromMinutes(5).TotalSeconds
                };

                return Ok(responseJson);
            }
            //else
            //{
            //    return Ok("");
            //}
        }
    }

    public class AuthRequest
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
