using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="không để trống!")]
        public String FullName { get; set; }
        [Required(ErrorMessage = "không để trống!")]
        public String UserName { get; set; }
        [Required(ErrorMessage = "không để trống!")]
        public String Password { get; set; }
        [Required(ErrorMessage = "không để trống!")]
        public String Email { get; set; }
        [Required(ErrorMessage = "không để trống!")]
        public String Phone { get; set; }
        public String CountError { get; set; }
        public String Roles { get; set; }
        public int? Gender { get; set; }
        public String Address { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int Status { get; set; }
    }
}
