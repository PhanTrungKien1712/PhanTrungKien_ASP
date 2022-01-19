using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    public class PostInfo
    {
        public int Id { get; set; }
        public int? TopicId { get; set; }
        public String TopicName { get; set; }
        public String Title { get; set; }
        public String Slug { get; set; }
        public String Detail { get; set; }
        public String Img { get; set; }
        public String PostType { get; set; }
        public String MetaKey { get; set; }
        public String MetaDesc { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int Status { get; set; }
    }
}
