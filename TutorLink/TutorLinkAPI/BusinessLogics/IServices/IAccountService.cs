using DataLayer.Entities;
<<<<<<< HEAD
using DataLayer.Interfaces;
using System;
using System.Threading.Tasks;
using TutorLinkAPI.ViewModel;
=======
using System;
using System.Collections.Generic;
>>>>>>> Account-

namespace TutorLinkAPI.BusinessLogics.IServices
{
    public interface IAccountService
    {
<<<<<<< HEAD
        Task<AccountViewModel> GetAccountById(Guid id);
        Task<AccountViewModel> AddAccount(AddAccountViewModel addAccountViewModel);
        Task UpdateAccountById(Guid id, UpdateAccountViewModel updateAccountViewModel);
        Task DeleteAccount(Guid id);
        Task DeleteAccount(AccountViewModel account);
        IUser? GetAccountEntityByUsername(string username);
=======
        void AddNewAccount(string username, string password, string fullname, string email, string phone, string address, UserGenders gender);
        IEnumerable<Account> GetAllAccounts();
        Account GetAccountById(Guid accountId);
        void UpdateAccount(Guid accountId, string fullname, string email, string phone, string address, UserGenders gender);
        void DeleteAccount(Guid accountId);
        Account GetAccountEntityByUsername(string username);
>>>>>>> Account-
    }
}
