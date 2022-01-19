using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Suppliers")]
    public class Supplier
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        public String Slug { get; set; }
        public int? Orders { get; set; }
        [Required]
        public String FullName { get; set; }
        [Required]
        public String Phone { get; set; }
        [Required]
        public String Email { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int Status { get; set; }
    }
}
