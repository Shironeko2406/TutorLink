using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using TutorLinkAPI.BusinessLogics.IServices;
using TutorLinkAPI.ViewModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TutorLinkAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("{id}")]
        [Route("GetAccountById")]
        public async Task<IActionResult> GetAccountById(Guid id)
        {
            var account = await _accountService.GetAccountById(id);
            if (account == null)
            {
                return NotFound();
            }

            var accountViewModel = new AccountViewModel
            {
                AccountId = account.AccountId,
                Username = account.Username,
                Fullname = account.Fullname,
                Email = account.Email,
                Phone = account.Phone,
                Address = account.Address,
                Gender = account.Gender
            };

            return Ok(accountViewModel);
        }

        [HttpPost]
        [Route("AddNewAccount")]
        public async Task<IActionResult> AddAccount(Guid id, AddAccountViewModel addAccountModel)
        {
            var newAccount = await _accountService.AddAccount(id, addAccountModel);
            if (newAccount == null)
            {
                return BadRequest("Failed to add new account!");
            }

            return CreatedAtAction(nameof(GetAccountById), new { id = newAccount.AccountId }, newAccount);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(Guid id, [FromBody] UpdateAccountViewModel updateModel)
        {
            await _accountService.UpdateAccount(id, updateModel);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            await _accountService.DeleteAccount(id);

            return NoContent();
        }
    }
}
