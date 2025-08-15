using ShopAccAPI.Dtos.User;

namespace ShopAccAPI.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToUserDto(this Models.User user)
        {
            return new UserDto
            {

                Id = user.Id,
                Username = user.Username,
                PasswordHash = user.PasswordHash,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.DateOfBirth,
                Balance = user.Balance,
                CreatedAt = user.CreatedAt,
                Role = user.Role,
                AvatarUrl = user.AvatarUrl,
                CountOrder = user.Orders?.Count ?? 0 
            };
        }
        public static Models.User ToCreateUser(this CreateUserDto userDto)
        {
            return new Models.User
            {
                Username = userDto.Username,
                PasswordHash = userDto.PasswordHash,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                DateOfBirth = userDto.DateOfBirth,
                Balance = userDto.Balance,
                Role = userDto.Role,
                CreatedAt = DateTime.UtcNow, // Set current time as created at
                AvatarUrl = userDto.AvatarUrl
            };
        }
    }
}
