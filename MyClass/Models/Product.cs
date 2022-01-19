using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public int? CatId { get; set; }
        [Required]
        public int SupplierId { get; set; }
        [Required(ErrorMessage = "Tên sản phẩm không để trống!")]
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Detail { get; set; }
        public string Img { get; set; }
        public double Price { get; set; }
        public double PriceSale { get; set; }
        public int Number { get; set; }
        public string MetaKey { get; set; }
        public string MetaDesc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int Status { get; set; }
    }
}
