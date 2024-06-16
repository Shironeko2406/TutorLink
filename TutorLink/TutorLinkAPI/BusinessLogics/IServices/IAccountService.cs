using DataLayer.Entities;
using System;
using System.Collections.Generic;

namespace TutorLinkAPI.BusinessLogics.IServices
{
    public interface IAccountService
    {
        Account AddNewAccount(string username, string password, string fullname, string email, string phone, string address, UserGenders gender);
        IEnumerable<Account> GetAllAccounts();
        Account GetAccountById(Guid accountId);
        void UpdateAccount(Guid id, string username, string password, string fullname, string email, string phone, string address, UserGenders gender);
        void DeleteAccount(Guid accountId);
        Account GetAccountEntityByUsername(string username);
        Account GetAccountEntityByUserId(Guid userId);
    }
}
