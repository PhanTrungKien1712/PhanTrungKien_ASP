using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("OrderDetails")]
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Number { get; set; }
        public double Price { get; set; }
        public int Qty { get; set; }//số lượng
        public double Amount { get; set; }//thành tiền
        public double Sale { get; set; }
        public String DeliveryName { get; set; }
        public String DeliveryEmail { get; set; }
        public String DeliveryPhone { get; set; }
        public String DeliveryAddress { get; set; }
        public int Status { get; set; }
    }
}
