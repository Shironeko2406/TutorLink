using DataLayer.DAL;
using DataLayer.DAL.Repositories;
using DataLayer.Entities;
using Microsoft.Identity.Client;
using TutorLinkAPI.BusinessLogics.IServices;

namespace TutorLinkAPI.BusinessLogics.Services;

public class AccountServices : IAccountService
{
    private readonly AccountRepository _accountRepository;
    private readonly TutorDbContext _context;

    public AccountServices(AccountRepository accountRepository, TutorDbContext context)
    {
        _accountRepository = accountRepository;
        _context = context;
    }

    #region Using entity
    public Account GetAccountEntityByUsername(string username)
    {
        var account = _accountRepository.Get(a => a.Username == username);
        return account;
    }

    public Account GetAccountEntityByUserId(Guid userId)
    {
        var account = _accountRepository.Get(a => a.AccountId == userId);
        return account;
    }
    #endregion
    public Account AddNewAccount(string username, string password, string fullname, string email, string phone, string address, UserGenders gender)
    {
        var newAccount = new Account
        {
            AccountId = Guid.NewGuid(),
            Username = username,
            Password = password, // Ideally, this should be hashed
            Fullname = fullname,
            Email = email,
            Phone = phone,
            Address = address,
            Gender = gender,
        };

        _context.Accounts.Add(newAccount);
        _context.SaveChanges();

        return newAccount;
    }


    private string GetFullExceptionMessage(Exception ex)
    {
        if (ex == null) return string.Empty;
        var message = ex.Message;
        if (ex.InnerException != null)
        {
            message += " --> " + GetFullExceptionMessage(ex.InnerException);
        }
        return message;
    }

    #region View Account
    public IEnumerable<Account> GetAllAccounts()
    {
        try
        {
            return _accountRepository.GetAll() ?? new List<Account>();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving accounts: {ex.Message}", ex);
        }
    }
    #endregion

    #region Get account by Id
    public Account GetAccountById(Guid AccountId)
    {
        try
        {
            return _accountRepository.GetById(AccountId) ?? throw new Exception("Account not found.");
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving account: {ex.Message}", ex);
        }
    }
    #endregion

    #region Update account
    public void UpdateAccount(Guid id, string username, string password, string fullname, string email, string phone, string address, UserGenders gender)
    {
        var account = _context.Accounts.Find(id);
        if (account == null)
            throw new Exception("Account not found.");

        account.Username = username;
        account.Password = password; // Ensure password hashing in production
        account.Fullname = fullname;
        account.Email = email;
        account.Phone = phone;
        account.Address = address;
        account.Gender = gender;

        _context.SaveChanges();
    }

    #endregion

    #region Delete account with Id
    public void DeleteAccount(Guid AccountId)
    {
        var account = _accountRepository.GetById(AccountId);
        if (account != null)
        {
            _accountRepository.Delete(account.AccountId);
            _accountRepository.SaveChanges();
        }
    }
    #endregion
}