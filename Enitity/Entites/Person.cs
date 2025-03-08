using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementSystem.DATA.Entites
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; }

        [Required]
        [MaxLength(20,ErrorMessage = "FirstName is more than 20 letter")]
        public string? FirstName { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "LastName is more than 20 letter")]
        public string? LastName { get; set; }

        [Required]
        public string? Phone { get; set; }
        [MaxLength(50, ErrorMessage = "Address is more than 50 letter")]
        public string? Address { get; set; }


        //ADD RELATIONS 

        // Person => Order ; one to many => (Person) to (Order)
        public virtual ICollection<Order>? Orders { get; set; }

        //Person => PersonType
        [ForeignKey("PersonType")]
        public int PersonTypeId{ get; set; }
        public virtual PersonType? PersonType { get; set; } 

        //Person => Users
        public virtual ICollection<User>? Users { get; set; }

    }
}
