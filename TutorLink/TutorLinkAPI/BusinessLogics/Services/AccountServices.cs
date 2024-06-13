using AutoMapper;
using DataLayer.DAL.Repositories;
using DataLayer.Entities;
using DataLayer.Interfaces;
using TutorLinkAPI.BusinessLogics.IServices;
using TutorLinkAPI.ViewModel;

namespace TutorLinkAPI.BusinessLogics.Services
{
    public class AccountServices : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly AccountRepository _accountRepository;

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
}
