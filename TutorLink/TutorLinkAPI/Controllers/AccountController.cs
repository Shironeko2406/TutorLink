using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DataLayer.Entities;
using DataLayer.DAL.Repositories;
using AutoMapper;
using TutorLinkAPI.ViewModels;
using System;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly AccountRepository _repository;
    private readonly ILogger<AccountController> _logger;
    private readonly IMapper _mapper;

    public AccountController(AccountRepository repository, ILogger<AccountController> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    // GET: api/account
    [HttpGet]
    public ActionResult<IEnumerable<AccountViewModel>> GetAllAccounts()
    {
        try
        {
            var accounts = _repository.GetAll();
            var accountViewModels = _mapper.Map<IEnumerable<AccountViewModel>>(accounts);
            return Ok(accountViewModels);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all accounts");
            return StatusCode(500, "Internal server error");
        }
    }

    // GET: api/account/{id}
    [HttpGet("{id}")]
    public ActionResult<AccountViewModel> GetAccountById(Guid id)
    {
        try
        {
            var account = _repository.Get(a => a.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }
            var accountViewModel = _mapper.Map<AccountViewModel>(account);
            return Ok(accountViewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting account by id: {id}");
            return StatusCode(500, "Internal server error");
        }
    }

    // POST: api/account
    [HttpPost]
    public ActionResult<AccountViewModel> AddAccount(AccountViewModel accountViewModel)
    {
        try
        {
            var account = _mapper.Map<Account>(accountViewModel);
            account.AccountId = Guid.NewGuid(); // Generate a new GUID for AccountId

            _repository.Add(account);
            _repository.SaveChanges();

            var createdAccountViewModel = _mapper.Map<AccountViewModel>(account);
            return CreatedAtAction(nameof(GetAccountById), new { id = createdAccountViewModel.AccountId }, createdAccountViewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding account");
            return StatusCode(500, "Internal server error");
        }
    }

    // PUT: api/account/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateAccount(Guid id, AccountViewModel accountViewModel)
    {
        if (id != accountViewModel.AccountId)
        {
            return BadRequest();
        }

        try
        {
            var existingAccount = _repository.Get(a => a.AccountId == id);
            if (existingAccount == null)
            {
                return NotFound();
            }

            var account = _mapper.Map(accountViewModel, existingAccount);
            _repository.Update(account);
            _repository.SaveChanges();

            var updatedAccountViewModel = _mapper.Map<AccountViewModel>(account);
            return Ok(updatedAccountViewModel);  // Return the updated account
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating account with id: {id}");
            return StatusCode(500, "Internal server error");
        }
    }


    // DELETE: api/account/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteAccount(Guid id)
    {
        try
        {
            var account = _repository.Get(a => a.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }

            _repository.Delete(id);
            _repository.SaveChanges();

            return Ok(new { message = "Account deleted successfully" });  // Return a success message
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting account with id: {id}");
            return StatusCode(500, "Internal server error");
        }
    }

}
