using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    public class OrderInfo
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
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Number { get; set; }
        public double Price { get; set; }
        public int Qty { get; set; }//số lượng
        public double Amount { get; set; }//thành tiền
        public double Sale { get; set; }
    }
}
