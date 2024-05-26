
using API.Requests;
using API.Responses;
using API.Services;



public class GoogleLoginEndpoint : Endpoint<GoogleLoginRequest, GoogleLoginResponse>
{
    private IAuthService _authService;

    public GoogleLoginEndpoint(IAuthService authService)
    {
        _authService = authService;
    }

    public override void Configure()
    {
        Post("/api/google-login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GoogleLoginRequest req, CancellationToken ct)
    {
        var token = await _authService.LoginWithGoogle(req);
        if (token is null)
        {
            await SendUnauthorizedAsync(ct);
        }

        Response.Token = token;
        await SendOkAsync(Response);
    }
    }

   


