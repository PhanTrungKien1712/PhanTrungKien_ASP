using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Sliders")]
    public class Slider
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="không để trống!")]
        public String Name { get; set; }
        [Required]
        public String Url { get; set; }
        public String Img { get; set; }
        public int? Orders { get; set; }
        public String Position { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int Status { get; set; }
    }
}
