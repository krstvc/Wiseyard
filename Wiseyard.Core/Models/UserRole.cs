using System;
using System.Collections.Generic;
using System.Text;
using Wiseyard.Core.Services;

namespace Wiseyard.Core.Models
{
    public class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public User User
        {
            get { return UserService.GetUserById(UserId); }
        }
        public Role Role
        {
            get { return RoleService.GetRoleById(RoleId); }
        }
    }
}
