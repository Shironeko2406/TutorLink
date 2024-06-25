using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using TutorLinkAPI.BusinessLogics.IServices;
#pragma warning disable
namespace TutorLinkAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
   private readonly IRoleService _roleService;

   public RoleController(IRoleService roleService)
   {
      _roleService = roleService;
   }

   #region Get All Roles
   [HttpGet]
   [Route("roles")]
   public IActionResult GetAllRoles()
   {
      var allRoles = _roleService.GetAllRoles().ToList();
      return Ok(allRoles);
   }
   #endregion

   #region Get Role By Id
   [HttpGet]
   [Route("role-roleId/{id}")]
   public IActionResult GetRoleById(int id)
   {
      var existedRole = _roleService.GetRoleById(id);
      return Ok("Get Role by id success!");
   }
   #endregion

   #region Add New Role
   [HttpPost]
   [Route("add-role")]
   public IActionResult AddNewRole(Role newRole)
   {
      var addRole = _roleService.AddNewRole(newRole);
      if (addRole != null)
      {
         return Ok("Add new role success!");
      }

      return BadRequest("Failed to add new role! Please try again");
   }
   #endregion

   #region Update Role
   [HttpPut]
   [Route("update-role/{id}")]
   public IActionResult UpdateRole(int id, string roleName)
   {
      var checkRole = _roleService.UpdateRole(id, roleName);
      if (checkRole == null)
      {
         return BadRequest("RoleId not available! Please try again");
      }

      return Ok("Updated role success!");
   }
   #endregion
   
   #region Delete Role
   [HttpDelete]
   [Route("delete-role/{id}")]
   public IActionResult DeleteRole(int id)
   {
      return Ok("Deleted role successfully!");
   }
   #endregion
}