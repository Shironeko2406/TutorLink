using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using TutorLinkAPI.BusinessLogics.IServices;
using TutorLinkAPI.ViewModel;

namespace TutorLinkAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    public AccountController(IAccountService accountService)
    {
    _accountService = accountService; 
    }
    
    #region Add New Account
    [HttpPost]
    [Route("add-account")]
    public async Task<IActionResult> AddNewAccount(AddAccountViewModel newAccount)
    {
        var account = await _accountService.AddNewAccount(newAccount);
        if (account != null)
        {
            return Ok(account);
        }

        return BadRequest("Failed to add new account.");
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

    [HttpPut("update/{id}")]
    public IActionResult UpdateAccount(Guid id, [FromBody] AccountUpdateModel model)
    {
        _accountService.UpdateAccount(
            id,
            model.Username,
            model.Password,
            model.Fullname,
            model.Email,
            model.Phone,
            model.Address,
            model.Gender
        );
        return Ok("Account updated successfully.");
    }


    #region Delete account
    [HttpDelete("delete/{id}")]
    public IActionResult DeleteAccount(Guid id)
    {
        
            _accountService.DeleteAccount(id);
            return Ok("Account deleted successfully.");
        
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
{   public String Username { get; set; }
    public string Password { get; set; }
    public string Fullname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public UserGenders Gender { get; set; }
}
#endregion