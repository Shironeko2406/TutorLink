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
        Task<AccountViewModel> AddAccount(AddAccountViewModel addAccountViewModel);
        Task UpdateAccountById(Guid id, UpdateAccountViewModel updateAccountViewModel);
        Task DeleteAccount(Guid id);
        Task DeleteAccount(AccountViewModel account);
        IUser? GetAccountEntityByUsername(string username);
    }
}
