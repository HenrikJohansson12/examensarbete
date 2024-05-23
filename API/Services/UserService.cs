using API.Requests;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public interface IUserService
{

    Task<bool> UpdateUserInformation(UpdateUserInformationRequest req, string userName);
}

public class UserService: IUserService
{
    private WebApiDbContext _dbContext;

    public UserService(WebApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public static List<User> listOfUsers = new List<User>();
    public async Task<bool> UpdateUserInformation(UpdateUserInformationRequest req, string userName)
    {
        var userToUpdate = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        if (userToUpdate is null)
        {
            return false;
        }
        if (! req.DisplayName.IsNullOrEmpty())
        {
            userToUpdate.DisplayName = req.DisplayName;
        }
        if (! req.ZipCode.IsNullOrEmpty())
        {
            userToUpdate.ZipCode = req.ZipCode;
        }

        await _dbContext.SaveChangesAsync();
        return true;
    }
}