using DataLayer.DAL.Repositories;
using DataLayer.Entities;
using TutorLinkAPI.BusinessLogics.IServices;

namespace TutorLinkAPI.BusinessLogics.Services;

public class AccountServices : IAccountService
{
    private readonly AccountRepository _accountRepository;

    public AccountServices(AccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    #region Using entity
    public Account GetAccountEntityByUsername(string username)
    {
        var account = _accountRepository.Get(a => a.Username == username);
        return account;
    }
    #endregion
}