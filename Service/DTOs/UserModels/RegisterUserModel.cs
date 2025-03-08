using SalesManagementSystem.SERVICE.DTOs.PersonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementSystem.SERVICE.DTOs.UserModels
{
    public class RegisterUserModel
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public int RoleId { get; set; }
        public PersonModel? PersonModel { get; set; }
    }
}
