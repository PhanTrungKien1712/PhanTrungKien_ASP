using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Topics")]
    public class Topic
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "không để trống!")]
        public int? ParentId { get; set; }
        public String Name { get; set; }
        public String Slug { get; set; }
        public int? Orders { get; set; }
        public String FullName { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public String UrlSite { get; set; }
        public String MetaKey { get; set; }
        public String MetaDesc { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int Status { get; set; }
    }
}
