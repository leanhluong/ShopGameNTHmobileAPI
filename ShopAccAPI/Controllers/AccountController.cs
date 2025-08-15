using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopAccAPI.Dtos.Account;
using ShopAccAPI.Interfaces;
using ShopAccAPI.Models;

namespace ShopAccAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepo;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepo = accountRepository;
        }
        [HttpGet("GetAllAccounts")]
        public async Task<IActionResult> GetAllAccounts()
        {
            var accounts = await _accountRepo.GetAllAccountsAsync();
            var accountDtos = accounts.Select(a => new
            {
                a.Id,
                a.Username,
                a.Password,
                a.Rank,
                a.Skin,
                a.Note,
                a.Price,
                a.IsSold,
                createdAt = a.CreatedAt.ToString("dd/MM/yyyy"),
                game = a.Game.Name
            });
            if (accounts == null || !accounts.Any())
            {
                return NotFound("No accounts found.");
            }
            return Ok(accountDtos);
        }

        [HttpGet("GetAccountById/{id}")]
        public async Task<IActionResult> GetAccountById([FromRoute] int id)
        {
            var account = await _accountRepo.GetAccountByIdAsync(id);
            if (account == null)
            {
                return NotFound($"Account with ID {id} not found.");
            }
            return Ok(new
            {
                account.Id,
                account.Rank,
                account.Skin,
                account.Note,
                account.Price,
                account.IsSold,
                createdAt = account.CreatedAt.ToString("dd/MM/yyyy"),
                game = account.Game.Name
            });
        }

        [HttpPost("CreateAccount")]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccDto accountDto)
        {
            if (accountDto == null)
            {
                return BadRequest("Account data is null.");
            }
            var newAccount = await _accountRepo.CreateAccountAsync(accountDto);
            return CreatedAtAction(nameof(GetAccountById), new { id = newAccount.Id }, new
            {
                message = "Account created successfully.",
                data = new
                {
                    newAccount.Id,
                    newAccount.Username,
                    newAccount.Rank,
                    newAccount.Skin,
                    newAccount.Note,
                    newAccount.Price,
                    newAccount.IsSold,
                    createdAt = newAccount.CreatedAt.ToString("dd/MM/yyyy"),
                    newAccount.GameId
                }

            });
        }
        [HttpPut("UpdateAccount/{id}")]
        public async Task<IActionResult> UpdateAccount([FromRoute] int id, [FromBody] UpdateAccDto accountDto)
        {
            if (accountDto == null)
            {
                return BadRequest("Account data is null.");
            }
            var updatedAccount = await _accountRepo.UpdateAccountAsync(id, accountDto);
            if (updatedAccount == null)
            {
                return NotFound($"Account with ID {id} not found.");
            }
            return Ok(new
            {
                message = "update account successfully",
                data = new
                {
                    updatedAccount.Username,
                    updatedAccount.Rank,
                    updatedAccount.Skin,
                    updatedAccount.Note,
                    updatedAccount.Price,
                    updatedAccount.IsSold,
                    createdAt = updatedAccount.CreatedAt.ToString("dd/MM/yyyy"),
                    updatedAccount.GameId
                }
            });
        }
        [HttpDelete("DeleteAccount/{id}")]
        public async Task<IActionResult> DeleteAccount([FromRoute] int id)
        {
            var deletedAccount = await _accountRepo.DeleteAccountAsync(id);
            if (deletedAccount == null)
            {
                return NotFound($"Account with ID {id} not found.");
            }
            return Ok(new { message = "Account deleted successfully" });
        }
    }
}
