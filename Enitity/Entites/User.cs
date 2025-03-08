using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementSystem.DATA.Entites
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string? UserName { get; set; }
        [Required]
        public string? PasswordHash{ get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        //User => Role ; Many to Many
        public virtual ICollection<Role>? Roles { get; set; }

        // User => Person
        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public virtual Person? Person { get; set; }

    }
}
