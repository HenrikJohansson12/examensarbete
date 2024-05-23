using FastEndpoints;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Database.Models;

public class GoogleLoginRequest
{
    public string Credential { get; set; }
}

public class GoogleLoginEndpoint : Endpoint<GoogleLoginRequest, GoogleLoginResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public GoogleLoginEndpoint(UserManager<User> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public override void Configure()
    {
        Post("/api/google-login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GoogleLoginRequest req, CancellationToken ct)
    {
        try
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(req.Credential, new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new List<string> { _configuration["Google:ClientId"] }
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
                    ThrowError(result.Errors.First().Description);
                    return;
                }
            }

            var token = GenerateJwtToken(user);
            await SendAsync(new GoogleLoginResponse { Token = token });
        }
        catch (InvalidJwtException)
        {
            ThrowError("Invalid Google credential.");
        }
    }

    private string GenerateJwtToken(IdentityUser user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public class GoogleLoginResponse
{
    public string Token { get; set; }
}
