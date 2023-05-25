using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace D7CRUD.NETCrud_Example.Models
{
    public class Category
    {
        public Int64 Id {get; set; }
        [Required]
        public String Name {get; set; }
        public String Description {get; set; }
    }
}