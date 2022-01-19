using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;
using PagedList;


namespace MyClass.DAO
{
    public class ProductDAO
    {
        private MyDBContext db = new MyDBContext();
        //Trả về danh sách các mẫu tin
        public List<Product> SearchByKey(string search)
        {
            return db.Products
                .Where(m => m.Name.ToLower().Contains(search))
                .ToList();
        }
        public List<ProductInfo> getListByCatId(int take, int? catid, int? notid = null)
        {
            List<ProductInfo> list = null;
            if (notid == null)
            {
                list = db.Products
                .Join(
                    db.Categorys,
                    p => p.CatId,
                    c => c.Id,
                    (p, c) => new ProductInfo
                    {
                        Id = p.Id,
                        CatId = p.CatId,
                        Name = p.Name,
                        CategoryName = c.Name,
                        Slug = p.Slug,
                        Detail = p.Detail,
                        Img = p.Img,
                        Price = p.Price,
                        PriceSale = p.PriceSale,
                        Number = p.Number,
                        MetaKey = p.MetaKey,
                        MetaDesc = p.MetaDesc,
                        CreatedBy = p.CreatedBy,
                        CreatedAt = p.CreatedAt,
                        UpdatedBy = p.UpdatedBy,
                        UpdatedAt = p.UpdatedAt,
                        Status = p.Status
                    }
                )
                .Where(m => m.Status == 1 && m.CatId == catid)
                .Take(take)
                .ToList();
            }
            else
            {
                list = db.Products
                .Join(
                    db.Categorys,
                    p => p.CatId,
                    c => c.Id,
                    (p, c) => new ProductInfo
                    {
                        Id = p.Id,
                        CatId = p.CatId,
                        Name = p.Name,
                        CategoryName = c.Name,
                        Slug = p.Slug,
                        Detail = p.Detail,
                        Img = p.Img,
                        Price = p.Price,
                        PriceSale = p.PriceSale,
                        Number = p.Number,
                        MetaKey = p.MetaKey,
                        MetaDesc = p.MetaDesc,
                        CreatedBy = p.CreatedBy,
                        CreatedAt = p.CreatedAt,
                        UpdatedBy = p.UpdatedBy,
                        UpdatedAt = p.UpdatedAt,
                        Status = p.Status
                    }
                )
                .Where(m => m.Status == 1 && m.CatId == catid && m.Id != notid)
                .Take(take)
                .ToList();
            }   
            return list;
        }
        public List<ProductInfo> getListByListCatId(List<int> listcatid, int take)
        {
            List<ProductInfo> list = db.Products
              .Join(
                    db.Categorys,
                    p => p.CatId,
                    c => c.Id,
                    (p, c) => new ProductInfo
                    {
                        Id = p.Id,
                        CatId = p.CatId,
                        Name = p.Name,
                        CategoryName = c.Name,
                        Slug = p.Slug,
                        Detail = p.Detail,
                        Img = p.Img,
                        Price = p.Price,
                        PriceSale = p.PriceSale,
                        Number = p.Number,
                        MetaKey = p.MetaKey,
                        MetaDesc = p.MetaDesc,
                        CreatedBy = p.CreatedBy,
                        CreatedAt = p.CreatedAt,
                        UpdatedBy = p.UpdatedBy,
                        UpdatedAt = p.UpdatedAt,
                        Status = p.Status
                    }
                )
                .Where(m => m.Status == 1 && listcatid.Contains((int)m.CatId))
                .OrderByDescending(m=>m.CreatedAt)
                .Take(take)
                .ToList();
            return list;
        }
        public IPagedList<ProductInfo> getListByListCatId(List<int> listcatid, int pageSize, int pageNumber)
        {
            IPagedList<ProductInfo> list = db.Products
              .Join(
                    db.Categorys,
                    p => p.CatId,
                    c => c.Id,
                    (p, c) => new ProductInfo
                    {
                        Id = p.Id,
                        CatId = p.CatId,
                        Name = p.Name,
                        CategoryName = c.Name,
                        Slug = p.Slug,
                        Detail = p.Detail,
                        Img = p.Img,
                        Price = p.Price,
                        PriceSale = p.PriceSale,
                        Number = p.Number,
                        MetaKey = p.MetaKey,
                        MetaDesc = p.MetaDesc,
                        CreatedBy = p.CreatedBy,
                        CreatedAt = p.CreatedAt,
                        UpdatedBy = p.UpdatedBy,
                        UpdatedAt = p.UpdatedAt,
                        Status = p.Status
                    }
                )
                .Where(m => m.Status == 1 && listcatid.Contains((int)m.CatId))
                .OrderByDescending(m => m.CreatedAt)
                .ToPagedList(pageNumber, pageSize);
            return list;
        }
        public List<ProductInfo> getList(int take)
        {
            List<ProductInfo> list = db.Products
              .Join(
                    db.Categorys,
                    p => p.CatId,
                    c => c.Id,
                    (p, c) => new ProductInfo
                    {
                        Id = p.Id,
                        CatId = p.CatId,
                        Name = p.Name,
                        CategoryName = c.Name,
                        Slug = p.Slug,
                        Detail = p.Detail,
                        Img = p.Img,
                        Price = p.Price,
                        PriceSale = p.PriceSale,
                        Number = p.Number,
                        MetaKey = p.MetaKey,
                        MetaDesc = p.MetaDesc,
                        CreatedBy = p.CreatedBy,
                        CreatedAt = p.CreatedAt,
                        UpdatedBy = p.UpdatedBy,
                        UpdatedAt = p.UpdatedAt,
                        Status = p.Status
                    }
                )
                .Where(m => m.Status == 1)
                .OrderByDescending(m => m.CreatedAt)
                .Take(take)
                .ToList();
            return list;
        }
        public IPagedList<ProductInfo> getList(int pageSize, int pageNumber)
        {
            IPagedList<ProductInfo> list = db.Products
              .Join(
                    db.Categorys,
                    p => p.CatId,
                    c => c.Id,
                    (p, c) => new ProductInfo
                    {
                        Id = p.Id,
                        CatId = p.CatId,
                        Name = p.Name,
                        CategoryName = c.Name,
                        Slug = p.Slug,
                        Detail = p.Detail,
                        Img = p.Img,
                        Price = p.Price,
                        PriceSale = p.PriceSale,
                        Number = p.Number,
                        MetaKey = p.MetaKey,
                        MetaDesc = p.MetaDesc,
                        CreatedBy = p.CreatedBy,
                        CreatedAt = p.CreatedAt,
                        UpdatedBy = p.UpdatedBy,
                        UpdatedAt = p.UpdatedAt,
                        Status = p.Status
                    }
                )
                .Where(m => m.Status == 1)
                .OrderByDescending(m => m.CreatedAt)
                .ToPagedList(pageNumber, pageSize);
            return list;
        }    
        public List<ProductInfo> getListJoin(string status = "All")
        {
            List<ProductInfo> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Products.Join(
                            db.Categorys,
                            p => p.CatId,
                            c => c.Id,
                            (p, c) => new ProductInfo
                            {
                                Id = p.Id,
                                CatId = p.CatId,
                                Name = p.Name,
                                CategoryName = c.Name,
                                Slug = p.Slug,
                                Detail = p.Detail,
                                Img = p.Img,
                                Price = p.Price,
                                PriceSale = p.PriceSale,
                                Number = p.Number,
                                MetaKey = p.MetaKey,
                                MetaDesc = p.MetaDesc,
                                CreatedBy = p.CreatedBy,
                                CreatedAt = p.CreatedAt,
                                UpdatedBy = p.UpdatedBy,
                                UpdatedAt = p.UpdatedAt,
                                Status = p.Status
                            }
                           ).Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Products.Join(
                            db.Categorys,
                            p => p.CatId,
                            c => c.Id,
                            (p, c) => new ProductInfo
                            {
                                Id = p.Id,
                                CatId = p.CatId,
                                Name = p.Name,
                                CategoryName = c.Name,
                                Slug = p.Slug,
                                Detail = p.Detail,
                                Img = p.Img,
                                Price = p.Price,
                                PriceSale = p.PriceSale,
                                Number = p.Number,
                                MetaKey = p.MetaKey,
                                MetaDesc = p.MetaDesc,
                                CreatedBy = p.CreatedBy,
                                CreatedAt = p.CreatedAt,
                                UpdatedBy = p.UpdatedBy,
                                UpdatedAt = p.UpdatedAt,
                                Status = p.Status
                            }
                           ).Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Products.Join(
                            db.Categorys,
                            p => p.CatId,
                            c => c.Id,
                            (p, c) => new ProductInfo
                            {
                                Id = p.Id,
                                CatId = p.CatId,
                                Name = p.Name,
                                CategoryName = c.Name,
                                Slug = p.Slug,
                                Detail = p.Detail,
                                Img = p.Img,
                                Price = p.Price,
                                PriceSale = p.PriceSale,
                                Number = p.Number,
                                MetaKey = p.MetaKey,
                                MetaDesc = p.MetaDesc,
                                CreatedBy = p.CreatedBy,
                                CreatedAt = p.CreatedAt,
                                UpdatedBy = p.UpdatedBy,
                                UpdatedAt = p.UpdatedAt,
                                Status = p.Status
                            }
                           ).ToList();
                        break;
                    }
            }
            return list;
        }
        public Product getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Products.Find(id);
            }
        }
        public Product getRow(string slug)
        {
            return db.Products.Where(m => m.Slug == slug && m.Status == 1).FirstOrDefault();
        }
        public int Insert(Product row)
        {
            db.Products.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin
        public int Update(Product row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xóa mẫu tin
        public int Delete(Product row)
        {
            db.Products.Remove(row);
            return db.SaveChanges();
        }
    }
}
