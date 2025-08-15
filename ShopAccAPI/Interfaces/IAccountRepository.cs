using ShopAccAPI.Dtos.Account;
using ShopAccAPI.Models;

namespace ShopAccAPI.Interfaces
{
    public interface IAccountRepository
    {
        Task<List<Account>> GetAllAccountsAsync();
        Task<Account?> GetAccountByIdAsync(int id);
        Task<Account> CreateAccountAsync(CreateAccDto account);
        Task<Account> UpdateAccountAsync(int id, UpdateAccDto account);
        Task<Account> DeleteAccountAsync(int id);
    }
}
