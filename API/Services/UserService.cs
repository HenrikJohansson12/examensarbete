using API.Requests;
using Database.Models;

namespace API.Services;

public interface IUserService
{
    Task<bool> SignUpUser(SignUpRequest req);
}

public class UserService: IUserService
{
    public static List<User> listOfUsers = new List<User>();
    public async Task<bool> SignUpUser(SignUpRequest req)
    {
        var newUser = new User()
        {
            EmailAddress = req.EmailAddress,
            DisplayName = req.DisplayName,
            Password = req.Password
        };
        
        //Todo Check if email or displayName exist in db. 
        
        listOfUsers.Add(newUser);
        return true;
    }
}