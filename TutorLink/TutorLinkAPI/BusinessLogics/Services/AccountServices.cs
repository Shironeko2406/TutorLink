using DataLayer.DAL;
using DataLayer.DAL.Repositories;
using DataLayer.Entities;
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
    /*public void Save()
    {
        _context.SaveChanges();
    }*/
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
                RoleId = 1 // Assuming a default RoleId, adjust as necessary
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
        return _accountRepository.GetAll();
    }
    #endregion

    #region Get account by Id
    public Account GetAccountById(Guid AccountId)
    {
        return _accountRepository.GetById(AccountId);
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
            _accountRepository.SaveChangesAsync();
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
            _accountRepository.SaveChangesAsync();
        }
    }
    #endregion
}
