using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using TutorLinkAPI.BusinessLogics.IServices;

namespace TutorLinkAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class RoleController : Controller
{
   private readonly IRoleService _roleService;

   public RoleController(IRoleService roleService)
   {
      _roleService = roleService;
   }

   #region Get All Roles
   [HttpGet]
   [Route("GetAllRoles")]
   public IActionResult GetAllRoles()
   {
      var allRoles = _roleService.GetAllRoles().ToList();
      return Ok(allRoles);
   }
   #endregion

   #region Get Role By Id
   [HttpGet]
   [Route("GetRoleById/{id}")]
   public IActionResult GetRoleById(int id)
   {
      var existedRole = _roleService.GetRoleById(id);
      return Ok("Get Role by id success!");
   }
   #endregion

   #region Add New Role
   [HttpPost]
   [Route("AddNewRole")]
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
   [Route("UpdateRole/{id}")]
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
}