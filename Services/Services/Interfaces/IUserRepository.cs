using Services.Models;

namespace Services.Services.Interfaces;

public interface IUserRepository
{
    Task<User?> Register(User user);
    Task<bool> Login(LoginModel user);
    Task<IEnumerable<User>> GetUsers();
    Task<User> GetUserById(Guid userId);
}