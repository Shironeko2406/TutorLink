using System.Collections.Generic;
using DataLayer.Entities;

namespace TutorLinkAPI.BusinessLogics.IServices
{
    public interface IAccountService
    {
        List<Account> GetAllAccounts();
        Account GetAccountById(Guid accountId);
        void CreateAccount(Account account);
        void UpdateAccount(Account account);
        void DeleteAccount(Guid accountId);
    }
}
