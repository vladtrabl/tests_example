using Services.Models;

namespace Services.Services.Interfaces;

public interface IUserService
{
    Task<User?> Register(User user);
    Task<bool> Login(LoginModel loginModel);
    Task<List<User>> GetUsers();
    Task<User?> GetUserById(Guid userId);
}