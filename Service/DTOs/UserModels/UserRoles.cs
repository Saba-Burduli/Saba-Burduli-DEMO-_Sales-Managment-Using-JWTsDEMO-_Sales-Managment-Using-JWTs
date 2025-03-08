﻿using SalesManagementSystem.DATA.Entites;
using SalesManagementSystem.SERVICE.DTOs.PersonModels;
using SalesManagementSystem.SERVICE.DTOs.RoleModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementSystem.SERVICE.DTOs.UserModels
{
    public class UserRolesModel
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public List<RoleModel> Roles { get; set; } = new List<RoleModel>();
    }
}
