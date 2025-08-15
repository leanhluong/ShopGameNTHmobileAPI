using Microsoft.EntityFrameworkCore;
using ShopAccAPI.Data;
using ShopAccAPI.Dtos.User;
using ShopAccAPI.Interfaces;
using ShopAccAPI.Mappers;
using ShopAccAPI.Models;

namespace ShopAccAPI.Respository
{
    public class UserRepository : IUserRepository
    {
        private readonly DBContext _context;
        public UserRepository(DBContext context)
        {
            _context = context;
        }
        public async Task<User> CreateUserAsync(CreateUserDto user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
            if (existingUser != null)
            {
                throw new InvalidOperationException($"User with username '{user.Username}' already exists.");
            }
            var newUser = user.ToCreateUser();
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }

        public async Task<User> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.Orders)
                .ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            var user = await _context.Users
                .Include(u => u.Orders)
                .FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            return user;

        }
        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            var user = await _context.Users
                .Include(u => u.Orders)
                .FirstOrDefaultAsync(u => u.Username == username);
            return user;
        }

        public async Task<User> UpdateUserAsync(int id, CreateUserDto user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (existingUser == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
            existingUser.Username = user.Username;
            existingUser.PasswordHash = user.PasswordHash;
            existingUser.Email = user.Email;
            existingUser.PhoneNumber = user.PhoneNumber;
            existingUser.DateOfBirth = user.DateOfBirth;
            existingUser.Balance = user.Balance;
            existingUser.Role = user.Role;
            existingUser.AvatarUrl = user.AvatarUrl;

            await _context.SaveChangesAsync();
            return existingUser;
        }
    }
}
