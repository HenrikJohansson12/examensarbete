using API.Responses;
using API.Services;
using Microsoft.AspNetCore.Identity.Data;

namespace API.Endpoints;

public class LoginEndpoint: Endpoint<LoginRequest, LoginResponse>
{
    private IAuthService _authService;

 public  LoginEndpoint(IAuthService authService)
    {
        _authService = authService;
    }
    
    public override void Configure()
    {
        Post("/api/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {

        var token = await _authService.Login(req);
        if (token is null)
        {
            await SendUnauthorizedAsync(ct);
        }

        Response.AccessToken = token;
        await SendOkAsync(Response);
    }

}