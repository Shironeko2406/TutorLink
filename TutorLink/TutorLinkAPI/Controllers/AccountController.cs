using Microsoft.AspNetCore.Mvc;
using DataLayer.Entities;
using TutorLinkAPI.BusinessLogics.IServices;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Account>> GetAllAccounts()
    {
        var accounts = _accountService.GetAllAccounts();
        return Ok(accounts);
    }

    [HttpGet("{id}")]
    public ActionResult<Account> GetAccountById(Guid id)
    {
        var account = _accountService.GetAccountById(id);
        if (account == null)
        {
            return NotFound();
        }
        return Ok(account);
    }

    [HttpPost]
    public ActionResult<Account> AddAccount(Account account)
    {
        _accountService.CreateAccount(account);
        return CreatedAtAction(nameof(GetAccountById), new { id = account.AccountId }, account);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAccount(Guid id, Account account)
    {
        if (id != account.AccountId)
        {
            return BadRequest();
        }

        var existingAccount = _accountService.GetAccountById(id);
        if (existingAccount == null)
        {
            return NotFound();
        }

        _accountService.UpdateAccount(account);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAccount(Guid id)
    {
        var account = _accountService.GetAccountById(id);
        if (account == null)
        {
            return NotFound();
        }

        _accountService.DeleteAccount(id);
        return NoContent();
    }
}
