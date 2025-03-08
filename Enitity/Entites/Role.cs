using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementSystem.DATA.Entites
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        [Required]
        public string? RoleName { get; set; }
        [Required]
        public string? RoleDescription { get; set; }

        //Role => User
        public virtual ICollection<User>? Users { get; set; }        
    }
}
