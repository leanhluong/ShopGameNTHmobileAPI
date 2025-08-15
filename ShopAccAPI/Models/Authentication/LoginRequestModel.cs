namespace ShopAccAPI.Models.Authentication
{
    public class LoginRequestModel
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool RememberMe { get; set; } = false;
    }
}
