using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementSystem.DATA.Entites
{
    public class PersonType
    {
        [Key]
        public int PersontypeId { get; set; }
        public string? PersonTypeName { get; set; } //add seeding  +A for 
        public string? PersonTypeDescription { get; set; }
        //PersonType => Person
        public virtual ICollection<Person>? Persons { get; set; }
    }
}
