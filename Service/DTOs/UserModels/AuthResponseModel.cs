using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementSystem.SERVICE.DTOs.UserModels
{
    public class AuthResponseModel
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
    }
}
