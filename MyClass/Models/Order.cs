using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Orders")] 
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public String ReceriverName { get; set; }
        public String ReceriverAddress { get; set; }
        public String ReceriverEmail { get; set; }
        public String ReceriverPhone { get; set; }
        public String Note { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int Status { get; set; }

    }
}
