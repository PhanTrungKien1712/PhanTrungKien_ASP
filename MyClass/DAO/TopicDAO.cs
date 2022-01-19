using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;

namespace MyClass.DAO
{
    public class TopicDAO
    {
        private MyDBContext db = new MyDBContext();
        //Trả về danh sách các mẫu tin
        public List<Topic> getListByParentId(int parentid = 0)
        {
            return db.Topics
                .Where(m => m.ParentId == parentid && m.Status == 1)
                .OrderBy(m => m.Orders)
                .ToList();
        }

        public List<Topic> getList(string status = "All")
        {
            List<Topic> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Topics.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Topics.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Topics.ToList();
                        break;
                    }
            }
            return list;
        }
        //Trả về danh sách 1 mẫu tin
        public Topic getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Topics.Find(id);
            }
        }
        public Topic getRow(string slug)
        {
            return db.Topics.Where(m => m.Slug == slug && m.Status == 1).FirstOrDefault();
        }
        //Thêm mẫu tin
        public int Insert(Topic row)
        {
            db.Topics.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin
        public int Update(Topic row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xóa mẫu tin
        public int Delete(Topic row)
        {
            db.Topics.Remove(row);
            return db.SaveChanges();
        }
    }
}
