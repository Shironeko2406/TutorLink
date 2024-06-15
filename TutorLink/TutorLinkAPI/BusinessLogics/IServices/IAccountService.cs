using DataLayer.Entities;

namespace TutorLinkAPI.BusinessLogics.IServices;

public interface IAccountService
{
    Account GetAccountEntityByUsername(string username);
    
    Account GetAccountEntityByUserId(Guid userId);
}