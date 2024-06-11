using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public interface IUser
    {
        Guid UserId { get;}
        string Username { get;}
        string Email { get;}
        int RoleId { get;}
    }
}
