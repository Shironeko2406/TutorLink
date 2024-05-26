using DataLayer.Entities;

namespace TutorLinkAPI.BusinessLogics.IServices;

public interface IRoleService
{
    public List<Role> GetAllRoles();
    public Role GetRoleById(int id);
    public Role AddNewRole(Role newRole);
    public Role UpdateRole(int id, string roleName);
}