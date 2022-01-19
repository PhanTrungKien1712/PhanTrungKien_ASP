using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Posts")]
    public class Post
    {
        public int Id { get; set; }
        public int? TopicId { get; set; }
        [Required(ErrorMessage = "Tiêu đề bài viết không để rỗng!")]
        public String Title { get; set; }
        public String Slug { get; set; }
        [Required]
        public String Detail { get; set; }
        public String Img { get; set; }
        public String PostType { get; set; }
        [Required]
        public String MetaKey { get; set; }
        [Required]
        public String MetaDesc { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int Status { get; set; }

    }
}
