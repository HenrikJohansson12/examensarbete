namespace API.Responses;

public class GoogleLoginResponse
{
    public string Message { get; set; }
    public List<string> Errors { get; set; }
}