using DataLayer.Entities;
using DataLayer.Interfaces;
using System;
using System.Threading.Tasks;
using TutorLinkAPI.ViewModel;

namespace TutorLinkAPI.BusinessLogics.IServices
{
    public interface IAccountService
    {
        Task<AccountViewModel> GetAccountById(Guid id);
        Task<AccountViewModel> AddAccount(Guid id, AddAccountViewModel addAccountModel);
        Task UpdateAccount(Guid id, UpdateAccountViewModel updateModel);
        Task DeleteAccount(Guid id);
        Task DeleteAccount(AccountViewModel account);
        IUser? GetAccountEntityByUsername(string username);
    }
}

    