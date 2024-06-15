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
    #region Add new account
    public void AddNewAccount(string Username, string Password, string Fullname, string Email, string Phone, string Address, UserGenders Gender)
    {
        
        
            var account = new Account
            {
                AccountId = Guid.NewGuid(),
                Username = Username,
                Password = Password,
                Fullname = Fullname,
                Email = Email,
                Phone = Phone,
                Address = Address,
                Gender = Gender,
                RoleId = 1 
            };

            _accountRepository.Add(account);
            _accountRepository.SaveChangesAsync();
       
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
    #endregion
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
    public void UpdateAccount(Guid AccountId, string Fullname, string Email, string Phone, string Address, UserGenders Gender)
    {
        var account = _accountRepository.GetById(AccountId);
        if (account != null)
        {
            account.Fullname = Fullname;
            account.Email = Email;
            account.Phone = Phone;
            account.Address = Address;
            account.Gender = Gender;

            _accountRepository.Update(account);
            _accountRepository.SaveChanges();
        }
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
