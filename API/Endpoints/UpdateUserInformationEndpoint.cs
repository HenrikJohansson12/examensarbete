using System.Security.Claims;
using API.Properties.Services;
using API.Requests;
using API.Services;

namespace API.Endpoints;

public class UpdateUserInformationEndpoint: Endpoint<UpdateUserInformationRequest>
{
        private IUserService _userService;

        public UpdateUserInformationEndpoint(IUserService userService)
        {
                _userService = userService;
        }
        
        public override void Configure()
        {
                Post("/api/updateuserinformation");
        }

        public override async Task HandleAsync(UpdateUserInformationRequest req, CancellationToken ct)
        {
                var userId = User.FindFirstValue((ClaimTypes.NameIdentifier));
              
                
                if (await _userService.UpdateUserInformation(req, userId))
                {
                        await SendNoContentAsync(ct);
                }
                else await SendAsync(500, cancellation: ct);
        }
}