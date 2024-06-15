using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using TutorLinkAPI.BusinessLogics.IServices;
<<<<<<< HEAD
using TutorLinkAPI.ViewModel;
using System;
using System.Threading.Tasks;
=======
>>>>>>> Account-

namespace TutorLinkAPI.Controllers
{
<<<<<<< HEAD
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
=======
    private readonly IAccountService _accountService;
    public AccountController(IAccountService accountService)
    {
    _accountService = accountService; 
    }

    [HttpGet]
    public IActionResult Index()
>>>>>>> Account-
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
<<<<<<< HEAD
}
=======
    #region Add new account
    [HttpPost("add")]
    public IActionResult AddNewAccount([FromBody] AccountRequestModel model)
    {
        try
        {
            _accountService.AddNewAccount(
                model.Username,
                model.Password,
                model.Fullname,
                model.Email,
                model.Phone,
                model.Address,
                model.Gender
            );
            return Ok("Account created successfully.");
        }
        catch (Exception ex)
        {
            // Log the complete exception message for debugging purposes
            return BadRequest($"Failed to create account: {ex.Message}");
        }
    }


    #endregion

    #region Get list
    [HttpGet("list")]
    public IActionResult ShowAccountList()
    {
        var account = _accountService.GetAllAccounts();
        return Ok(account);
    }
    #endregion

    #region View account with Id
    [HttpGet("get/{id}")]
    public IActionResult GetAccountById(Guid id)
    {
        var account = _accountService.GetAccountById(id);
        if (account == null)
            return NotFound("Account not found.");

        return Ok(account);
    }
    #endregion

    #region Update Account
    [HttpPut("update/{id}")]
    public IActionResult UpdateAccount(Guid id, [FromBody] AccountUpdateModel model)
    {
        try
        {
            _accountService.UpdateAccount(
                id,
                model.Fullname,
                model.Email,
                model.Phone,
                model.Address,
                model.Gender
            );
            return Ok("Account updated successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to update account: {ex.Message}");
        }
    }
    #endregion

    #region Delete account
    [HttpDelete("delete/{id}")]
    public IActionResult DeleteAccount(Guid id)
    {
        try
        {
            _accountService.DeleteAccount(id);
            return Ok("Account deleted successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to delete account: {ex.Message}");
        }
    }
    #endregion

}
#region Account model
public class AccountRequestModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Fullname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public UserGenders Gender { get; set; }
}

public class AccountUpdateModel
{
    public string Fullname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public UserGenders Gender { get; set; }
}
#endregion
>>>>>>> Account-
