using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;

namespace MyClass.DAO
{
    public class LinkDAO
    {
        private MyDBContext db = new MyDBContext();       
        //Trả về danh sách 1 mẫu tin
        public Link getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Links.Find(id);
            }
        }
        public Link getRow (string slug)
        {
            return db.Links.Where(m => m.Slug == slug).FirstOrDefault();
        }
        public Link getRow (int tableid, string type)
        {
            return db.Links.Where(m => m.TableId == tableid && m.Type == type).FirstOrDefault();
        }
        //Thêm mẫu tin
        public int Insert(Link row)
        {
            db.Links.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin
        public int Update(Link row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xóa mẫu tin
        public int Delete(Link row)
        {
            db.Links.Remove(row);
            return db.SaveChanges();
        }
    }
}

