using Services.Models;
using Services.Services.Interfaces;

namespace Services.Services.Implementation;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> Register(User user)
    {
        return await _userRepository.Register(user);
    }

    public async Task<bool> Login(LoginModel loginModel)
    {
        var users = await _userRepository.GetUsers();
        var user = users.FirstOrDefault(x => x.Email == loginModel.Login && x.Password == loginModel.Password);
        return user is not null;
    }

    public async Task<List<User>> GetUsers()
    {
        var users = await _userRepository.GetUsers();
        return users.ToList();
    }

    public async Task<User?> GetUserById(Guid userId)
    {
        var users = await _userRepository.GetUsers();
        return users.FirstOrDefault(x => x.Id == userId);
    }
}