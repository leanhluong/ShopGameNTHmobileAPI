using ShopAccAPI.Dtos.User;
using ShopAccAPI.Models;

namespace ShopAccAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<User> CreateUserAsync(CreateUserDto user);
        Task<User> UpdateUserAsync(int id, CreateUserDto user);
        Task<User> DeleteUserAsync(int id);
    }
}
