namespace API.Requests;

public class SignUpRequest
{
    public string EmailAddress { get; set; }
    public string Password { get; set; }
    public string DisplayName { get; set; }
}