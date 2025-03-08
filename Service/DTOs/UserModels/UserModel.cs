﻿using SalesManagementSystem.SERVICE.DTOs.PersonModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementSystem.SERVICE.DTOs.UserModels
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public PersonModel? Person { get; set; }
    }
}
