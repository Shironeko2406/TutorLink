using System.Collections.Generic;
using System.Linq;
using DataLayer.Entities;
using DataLayer.DAL.Repositories;
using TutorLinkAPI.BusinessLogics.IServices;

namespace TutorLinkAPI.BusinessLogics.Services
{
    public class AccountServices : IAccountService
    {
        private readonly AccountRepository _accountRepository;
        public AccountServices(AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public List<Account> GetAllAccounts() => _accountRepository.GetAll().ToList();

        public Account GetAccountById(Guid accountId) => _accountRepository.Get(a => a.AccountId == accountId);

        public void CreateAccount(Account account)
        {
            _accountRepository.Add(account);
            _accountRepository.SaveChanges();
        }

        public void UpdateAccount(Account account)
        {
            _accountRepository.Update(account);
            _accountRepository.SaveChanges();
        }

        public void DeleteAccount(Guid accountId)
        {
            _accountRepository.Delete(accountId);
            _accountRepository.SaveChanges();
        }
    }
}
