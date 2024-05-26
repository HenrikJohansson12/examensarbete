using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Database.Models;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public interface IAuthService
{
      Task<string> Login(LoginRequest req);
      Task<string> LoginWithGoogle(GoogleLoginRequest req);
    
}

public class AuthService: IAuthService
{ 
      private readonly UserManager<User> _userManager;
      private   IConfigurationRoot _config = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .Build();
      
      public AuthService(UserManager<User> userManager)
      {
            _userManager = userManager;
      }

      public async Task<string?>Login(LoginRequest user)
      {
            var identityUser = await _userManager.FindByEmailAsync(user.Email);
            if (identityUser is null)
            {
                  return null;
            }

            if ( !await _userManager.CheckPasswordAsync(identityUser, user.Password))
            {
                  return null;
            }
            return GenerateJwtToken(identityUser);
      }

      public async Task<string> LoginWithGoogle(GoogleLoginRequest req)
      {
            try
            {
                  var payload = await GoogleJsonWebSignature.ValidateAsync(req.Credential,
                        new GoogleJsonWebSignature.ValidationSettings
                        {
                              Audience = new List<string> { _config["Google:ClientId"] }
                        });

                  var user = await _userManager.FindByEmailAsync(payload.Email);
                  if (user == null)
                  {
                        user = new User()
                        {
                              UserName = payload.Email,
                              Email = payload.Email
                        };
                        var result = await _userManager.CreateAsync(user);
                        if (!result.Succeeded)
                        {
                              return null;
                        }
                  }

                  var token = GenerateJwtToken(user);
                  return token;
            }
            catch 
}

      
      private string GenerateJwtToken(IdentityUser user)
      {
            var claims = new[]
            {
                  new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                  new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                  issuer: _config["Jwt:Issuer"],
                  audience: _config["Jwt:Audience"],
                  claims: claims,
                  expires: DateTime.Now.AddMinutes(30),
                  signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
      }
}

