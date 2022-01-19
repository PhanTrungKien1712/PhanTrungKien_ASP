using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThoiTrangNam
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public double Price { get; set; }
        public int Qty { get; set; }
        public double Amount { get; set; }
        public CartItem() { }
        public CartItem(int proid, string name, string img, double price, int qty) {
            this.ProductId = proid;
            this.Name = name;
            this.Img = img;
            this.Price = price;
            this.Qty = qty;
            this.Amount = price * qty;
        }
    }
}