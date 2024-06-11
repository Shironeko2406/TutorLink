using AutoMapper;
using DataLayer.DAL.Repositories;
using DataLayer.Entities;
using TutorLinkAPI.BusinessLogics.IServices;
using TutorLinkAPI.ViewModel;
using System;
using System.Threading.Tasks;
using DataLayer.Interfaces;

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
            try
            {
                var account = await _accountRepository.GetByIdAsync(id);
                var accountViewModel = _mapper.Map<AccountViewModel>(account);
                return accountViewModel;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the account by Id.", ex);
            }
        }
        #endregion

        #region AddAccount
        public async Task<AccountViewModel> AddAccount(Guid id, AddAccountViewModel addAccountModel)
        {
            try
            {
                var account = _mapper.Map<Account>(addAccountModel);
                account.AccountId = id;

                await _accountRepository.AddSingleWithAsync(account);
                await _accountRepository.SaveChangesAsync();

                var addedAccountViewModel = _mapper.Map<AccountViewModel>(account);
                return addedAccountViewModel;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding a new account.", ex);
            }
        }
        #endregion

        #region UpdateAccount
        public async Task UpdateAccount(Guid id, UpdateAccountViewModel updateModel)
        {
            try
            {
                var account = await _accountRepository.GetByIdAsync(id);
                if (account == null)
                {
                    throw new Exception($"Account with ID {id} not found.");
                }

                _mapper.Map(updateModel, account);
                await _accountRepository.UpdateWithAsync(account);
                await _accountRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the account.", ex);
            }
        }
        #endregion

        #region DeleteAccount
        public async Task DeleteAccount(Guid id)
        {
            try
            {
                var account = await _accountRepository.GetByIdAsync(id);
                if (account == null)
                {
                    throw new Exception($"Account with ID {id} not found.");
                }

                _accountRepository.Remove(account);
                await _accountRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the account.", ex);
            }
        }

        public async Task DeleteAccount(AccountViewModel account)
        {
            try
            {
                var accountEntity = _mapper.Map<Account>(account);
                _accountRepository.Remove(accountEntity);
                await _accountRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the account.", ex);
            }
        }

        public IUser? GetAccountEntityByUsername(string username)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
