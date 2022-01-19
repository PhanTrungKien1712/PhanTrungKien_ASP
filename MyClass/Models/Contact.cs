using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Contacts")]
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Họ tên không để trống!")]
        public String FullName { get; set; }
        [Required(ErrorMessage = "Điện thoại không để trống!")]
        public String Phone { get; set; }
        [Required(ErrorMessage = "Email không để trống!")]
        public String Email { get; set; }
        [Required(ErrorMessage = "Tiêu đề không để trống!")]
        public String Title { get; set; }
        [Required(ErrorMessage = "Nội dung không để trống!")]
        public String Detail { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int Status { get; set; }
    }
}
