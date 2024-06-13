using Microsoft.AspNetCore.Mvc;
using TutorLinkAPI.BusinessLogics.IServices;
using TutorLinkAPI.ViewModel;
using System;
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

        #region Get Account By Id
        [HttpGet]
        [Route("GetAccountById/{id}")]
        public async Task<IActionResult> GetAccountById(Guid id)
        {
            try
            {
                var account = await _accountService.GetAccountById(id);
                if (account == null)
                {
                    return NotFound("Account not found.");
                }
                return Ok(account);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion

        #region Add New Account
        [HttpPost]
        [Route("AddNewAccount")]
        public async Task<IActionResult> AddNewAccount([FromBody] AddAccountViewModel newAccount)
        {
            if (newAccount == null)
            {
                return BadRequest("Account is null.");
            }

            try
            {
                var account = await _accountService.AddAccount(newAccount);
                if (account == null)
                {
                    return BadRequest("Failed to add new account.");
                }
                return Ok(account);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion

        #region Update Account By Id
        [HttpPut]
        [Route("UpdateAccountById/{id}")]
        public async Task<IActionResult> UpdateAccountById(Guid id,UpdateAccountViewModel updateAccount)
        {
            try
            {
                var account = await _accountService.UpdateAccountById(id, updateAccount);
                if (account == null)
                {
                    return BadRequest("Failed to update account.");
                }
                return Ok(account);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion

        #region Delete Account By Id
        [HttpDelete]
        [Route("DeleteAccountById/{id}")]
        public async Task<IActionResult> DeleteAccountById(Guid id)
        {
            try
            {
                await _accountService.DeleteAccount(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion

        #region Delete Account By Model
        [HttpDelete]
        [Route("DeleteAccount")]
        public async Task<IActionResult> DeleteAccount([FromBody] AccountViewModel account)
        {
            if (account == null)
            {
                return BadRequest("Account is null.");
            }

            try
            {
                await _accountService.DeleteAccount(account);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion

        #region Get Account By Username
        [HttpGet]
        [Route("GetAccountByUsername/{username}")]
        public IActionResult GetAccountByUsername(string username)
        {
            try
            {
                var account = _accountService.GetAccountEntityByUsername(username);
                if (account == null)
                {
                    return NotFound("Account not found.");
                }
                return Ok(account);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion
    }
}
