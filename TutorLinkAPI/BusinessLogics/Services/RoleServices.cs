using DataLayer.DAL.Repositories;
using DataLayer.Entities;
using TutorLinkAPI.BusinessLogics.IServices;

namespace TutorLinkAPI.BusinessLogics.Services;

public class RoleServices : IRoleService
{
    private readonly RoleRepository _roleRepository;

    public RoleServices(RoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }
    
    #region GetAllRoles()
    public List<Role> GetAllRoles()
    {
        try
        {
            var allRoles = _roleRepository.GetAll().ToList();
            return allRoles;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region GetRoleById()
    public Role GetRoleById(int id)
    {
        try
        {
            var currentRole = _roleRepository.Get(r => r.RoleId == id);
            if (currentRole == null)
            {
                throw new Exception("No role found with this ID");
            }

            return currentRole;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region AddNewRole()
    public Role AddNewRole(Role newRole)
    {
        try
        {
            var checkRole = _roleRepository.Get(r => r.RoleId == newRole.RoleId);
            if (checkRole != null)
            {
                throw new Exception("This RoleId already existed! Please try different id");
            }

            _roleRepository.Add(newRole);
            _roleRepository.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return newRole;
    }
    #endregion

    #region UpdateRole()

    public Role UpdateRole(int id, string roleName)
    {
        var existingRole = _roleRepository.Get(r => r.RoleId == id);
        if (existingRole == null)
        {
            throw new Exception("This RoleId not exist! Please try different id");
        }

        existingRole.RoleName = roleName;
        
        _roleRepository.Update(existingRole);
        return existingRole;
    }

    #endregion
    
}