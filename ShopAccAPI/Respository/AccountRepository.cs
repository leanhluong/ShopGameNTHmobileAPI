using Microsoft.EntityFrameworkCore;
using ShopAccAPI.Data;
using ShopAccAPI.Dtos.Account;
using ShopAccAPI.Interfaces;
using ShopAccAPI.Models;

namespace ShopAccAPI.Respository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DBContext _context;
        public AccountRepository(DBContext context)
        {
            _context = context;
        }
        public async Task<Account> CreateAccountAsync(CreateAccDto accountDto)
        {
            var newAccount = new Account
            {
                Username = accountDto.Username,
                Password = accountDto.Password, 
                Rank = accountDto.Rank,
                Skin = accountDto.Skin,
                Note = accountDto.Note,
                Price = accountDto.Price,
                IsSold = false,
                ImageUrl = accountDto.ImageUrl,
                GameId = accountDto.GameId,
                CreatedAt = DateTime.Now
            };
            _context.Accounts.Add(newAccount);
            await _context.SaveChangesAsync();
            return newAccount;
        }

        public async Task<Account> DeleteAccountAsync(int id)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id);
            if (account == null)
            {
                throw new KeyNotFoundException($"Account with ID {id} not found.");
            }
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<Account?> GetAccountByIdAsync(int id)
        {
            return await _context.Accounts
                .Include(a => a.Game)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Account>> GetAllAccountsAsync()
        {
            return await _context.Accounts.Include(a => a.Game).ToListAsync();
        }

        public async Task<Account> UpdateAccountAsync(int id, UpdateAccDto account)
        {
            var existingAccount = _context.Accounts.FirstOrDefault(a => a.Id == id);
            if (existingAccount == null)
            {
                throw new KeyNotFoundException($"Account with ID {id} not found.");
            }
            existingAccount.Username = account.Username;
            existingAccount.Password = account.Password;
            existingAccount.Rank = account.Rank;
            existingAccount.Skin = account.Skin;
            existingAccount.Note = account.Note;
            existingAccount.Price = account.Price;
            existingAccount.ImageUrl = account.ImageUrl;
            existingAccount.IsSold = account.IsSold;
            existingAccount.GameId = account.GameId;
            existingAccount.CreatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return existingAccount;
        }
    }
}
