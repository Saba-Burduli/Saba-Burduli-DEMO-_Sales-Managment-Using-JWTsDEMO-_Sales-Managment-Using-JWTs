using SalesManagementSystem.DATA.Entites;
using SalesManagementSystem.SERVICE.DTOs.PersonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementSystem.SERVICE.DTOs.UserModels
{
    public class UpdateUserModel
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
