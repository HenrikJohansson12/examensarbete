using API.Properties.Services;
using API.Requests;
using API.Services;

namespace API.Endpoints;

public class SignUpUserEndpoint: Endpoint<SignUpRequest>
{
        private IUserService _userService;

        public SignUpUserEndpoint(IUserService userService)
        {
                _userService = userService;
        }
        
        public override void Configure()
        {
                Get("/api/signupuser");
                AllowAnonymous();
        }

        public override async Task HandleAsync(SignUpRequest req, CancellationToken ct)
        {

                if (await _userService.SignUpUser(req))
                {
                        await SendNoContentAsync(ct);
                }

                else await SendAsync(500, cancellation: ct);
        }
}