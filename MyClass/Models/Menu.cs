using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Menus")]
    public class Menu
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên Menu không để rỗng")]
        public string Name { get; set; }
        [Required]
        public string Link { get; set; }
        public int? TableId { get; set; }
        public string TypeMenu { get; set; }
        public string Position { get; set; } 
        public int? ParentId { get; set; }
        public int? Orders { get; set; }
        public int? Type { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int Status { get; set; }
    }
}
