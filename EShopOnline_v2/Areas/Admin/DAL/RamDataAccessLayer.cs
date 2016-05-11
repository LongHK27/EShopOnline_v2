using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EShopOnline_v2.DAL;

namespace EShopOnline_v2.Areas.Admin.DAL
{
    public class RamDataAccessLayer
    {
        private static readonly DataClassesDataContext Db = new DataClassesDataContext();

        public static List<RAM> GetTable()
        {
            return (from a in Db.RAMs where a.ID != 0 select a).ToList();
        }

        public static bool Insert(RAM newProduct, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3, string severPath)
        {
            try
            {
                var file = new Extend.FileAccess();

                if (file.Save(file1, severPath, "RAM"))
                {
                    newProduct.LinkImg1 = "/Images/RAM/" + file1.FileName;
                }
                if (file.Save(file2, severPath, "RAM"))
                {
                    newProduct.LinkImg2 = "/Images/RAM/" + file2.FileName;
                }
                if (file.Save(file3, severPath, "RAM"))
                {
                    newProduct.LinkImg3 = "/Images/RAM/" + file3.FileName;
                }

                var list = (from a in Db.RAMs select a).ToList();

                newProduct.ID = list.Last().ID + 1;

                Db.RAMs.InsertOnSubmit(newProduct);
                Db.SubmitChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static RAM GetItem(int id)
        {
            return (from a in Db.RAMs where a.ID == id select a).SingleOrDefault();
        }

        public static bool Update(int id, RAM product, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3, string severPath)
        {
            try
            {
                var item = (from a in Db.RAMs where a.ID == id select a).SingleOrDefault();
                if (item == null) return false;

                item.BaoHanh    = product.BaoHanh;
                item.CongNghe   = product.CongNghe;
                item.DungLuong  = product.DungLuong;
                item.Gia        = product.Gia;
                item.GiamGia    = product.GiamGia;
                item.HangSX     = product.HangSX;
                item.Loai       = product.Loai;
                item.NamSX      = product.NamSX;
                item.SoLuong    = product.SoLuong;
                item.Ten        = product.Ten;
                item.XuatXu     = product.XuatXu;
                item.TanNhiet   = product.TanNhiet;
                

                var file = new Extend.FileAccess();
                if (file1?.FileName != null)
                {
                    file.Delete(item.LinkImg1, severPath, "RAM");
                    file.Save(file1, severPath, "RAM");
                    item.LinkImg1 = "/Image/RAM/" + file1.FileName;
                }
                if (file2?.FileName != null)
                {
                    file.Delete(item.LinkImg2, severPath, "RAM");
                    file.Save(file2, severPath, "RAM");
                    item.LinkImg2 = "/Image/RAM/" + file2.FileName;
                }
                if (file3?.FileName != null)
                {
                    file.Delete(item.LinkImg3, severPath, "RAM");
                    file.Save(file3, severPath, "RAM");
                    item.LinkImg3 = "/Image/RAM/" + file3.FileName;
                }

                Db.SubmitChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public static bool Delete(int id, string severPath)
        {
            try
            {
                var item = (from a in Db.RAMs where a.ID == id select a).SingleOrDefault();
                if (item == null) return false;

                var file = new Extend.FileAccess();
                var listFilename = item.LinkImg1.Split('/').ToList();
                file.Delete(listFilename[3], severPath, "RAM");

                listFilename = item.LinkImg2.Split('/').ToList();
                file.Delete(listFilename[3], severPath, "RAM");

                listFilename = item.LinkImg3.Split('/').ToList();
                file.Delete(listFilename[3], severPath, "RAM");

                Db.RAMs.DeleteOnSubmit(item);
                Db.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}