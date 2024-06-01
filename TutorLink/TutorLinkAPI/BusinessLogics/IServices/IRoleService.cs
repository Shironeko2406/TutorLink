using DataLayer.Entities;

namespace TutorLinkAPI.BusinessLogics.IServices;

public interface IRoleService
{
    List<Role> GetAllRoles();
    Role GetRoleById(int id);
    Role AddNewRole(Role newRole);
    Role UpdateRole(int id, string roleName);
}