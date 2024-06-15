<<<<<<< HEAD
using AutoMapper;
using DataLayer.DAL.Repositories;
using DataLayer.Entities;
using DataLayer.Interfaces;
=======
using DataLayer.DAL;
using DataLayer.DAL.Repositories;
using DataLayer.Entities;
using Microsoft.Identity.Client;
>>>>>>> Account-
using TutorLinkAPI.BusinessLogics.IServices;
using TutorLinkAPI.ViewModel;

namespace TutorLinkAPI.BusinessLogics.Services
{
<<<<<<< HEAD
    public class AccountServices : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly AccountRepository _accountRepository;
=======
    private readonly AccountRepository _accountRepository;
    private readonly TutorDbContext _context;

    public AccountServices(AccountRepository accountRepository, TutorDbContext context)
    {
        _accountRepository = accountRepository;
        _context = context;
    }
>>>>>>> Account-

        public AccountServices(IMapper mapper, AccountRepository accountRepository)
        {
            _mapper = mapper;
            _accountRepository = accountRepository;
        }

        #region GetAccountById
        public async Task<AccountViewModel> GetAccountById(Guid id)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            if (account == null)
            {
                throw new Exception("Account not found.");
            }

            return _mapper.Map<AccountViewModel>(account);
        }
        #endregion

        #region AddAccount
        public async Task<AccountViewModel> AddAccount(AddAccountViewModel addAccountModel)
        {
            var newAccount = _mapper.Map<Account>(addAccountModel);
            newAccount.AccountId = Guid.NewGuid();

            await _accountRepository.AddSingleWithAsync(newAccount);
            await _accountRepository.SaveChangesAsync();

            return _mapper.Map<AccountViewModel>(newAccount);
        }
        #endregion

        #region UpdateAccount
        public async Task UpdateAccountById(Guid id, UpdateAccountViewModel accountViewModel)
        {
            var Account = await _accountRepository.GetByIdAsync(id);
            if (Account == null)
            {
                throw new Exception("Account not found.");
            }

            _mapper.Map(accountViewModel, Account);
            await _accountRepository.UpdateWithAsync(Account);
            await _accountRepository.SaveChangesAsync();
        }
        #endregion

        #region DeleteAccount
        public async Task DeleteAccount(Guid id)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            if (account == null)
            {
                throw new Exception("Account not found.");
            }

            await _accountRepository.DeleteAsync(account);
            await _accountRepository.SaveChangesAsync();
        }

        public async Task DeleteAccount(AccountViewModel account)
        {
            var existingAccount = _mapper.Map<Account>(account);
            await _accountRepository.DeleteAsync(existingAccount);
            await _accountRepository.SaveChangesAsync();
        }
        #endregion

        #region GetAccountEntityByUsername
        public IUser? GetAccountEntityByUsername(string username)
        {
            return _accountRepository.Get(a => a.Username == username);
        }
        #endregion
    }
<<<<<<< HEAD
=======
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
>>>>>>> Account-
}
