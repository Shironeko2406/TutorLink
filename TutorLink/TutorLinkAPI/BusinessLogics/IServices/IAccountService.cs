using DataLayer.Entities;

namespace TutorLinkAPI.BusinessLogics.IServices;

public interface IAccountService
{
    Account GetAccountEntityByUsername(string username);
    void AddNewAccount(string Username, string Password, string Fullname, string Email, string Phone, string Address, UserGenders Gender);
    IEnumerable<Account> GetAllAccounts();
    Account GetAccountById(Guid AccountId);
    void UpdateAccount(Guid AccountId, string Fullname, string Email, string Phone, string Address, UserGenders Gender);
    void DeleteAccount(Guid AccountId);
    void Save();
}