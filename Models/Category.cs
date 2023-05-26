using System.ComponentModel.DataAnnotations;

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