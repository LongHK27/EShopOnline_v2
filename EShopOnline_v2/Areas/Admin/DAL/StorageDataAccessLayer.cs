using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EShopOnline_v2.DAL;

namespace EShopOnline_v2.Areas.Admin.DAL
{
    public class StorageDataAccessLayer
    {
        private static readonly DataClassesDataContext Db = new DataClassesDataContext();

        public static List<OCung> GetTable()
        {
            return (from a in Db.OCungs where a.ID != 0 select a).ToList();
        }

        public static bool Insert(OCung newProduct, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3, string severPath)
        {
            try
            {
                var file = new Extend.FileAccess();

                if (file.Save(file1, severPath, "Storage"))
                {
                    newProduct.LinkImg1 = "/Images/Storage/" + file1.FileName;
                }
                if (file.Save(file2, severPath, "Storage"))
                {
                    newProduct.LinkImg2 = "/Images/Storage/" + file2.FileName;
                }
                if (file.Save(file3, severPath, "Storage"))
                {
                    newProduct.LinkImg3 = "/Images/Storage/" + file3.FileName;
                }

                var list = (from a in Db.OCungs select a).ToList();

                newProduct.ID = list.Last().ID + 1;

                Db.OCungs.InsertOnSubmit(newProduct);
                Db.SubmitChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static OCung GetItem(int id)
        {
            return (from a in Db.OCungs where a.ID == id select a).SingleOrDefault();
        }

        public static bool Update(int id, OCung product, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3, string severPath)
        {
            try
            {
                var item = (from a in Db.OCungs where a.ID == id select a).SingleOrDefault();
                if (item == null) return false;

                item.BaoHanh        = product.BaoHanh;
                item.BoNhoDem       = product.BoNhoDem;
                item.ChuanKetNoi    = product.ChuanKetNoi;
                item.DungLuong      = product.DungLuong;
                item.Gia            = product.Gia;
                item.GiamGia        = product.GiamGia;
                item.HangSX         = product.HangSX;
                item.Loai           = product.Loai;
                item.KichThuoc      = product.KichThuoc;
                item.SoLuong        = product.SoLuong;
                item.Ten            = product.Ten;
                item.XuatXu         = product.XuatXu;
                item.TocDoVongQuay  = product.TocDoVongQuay;
               
                var file = new Extend.FileAccess();
                if (file1?.FileName != null)
                {
                    file.Delete(item.LinkImg1, severPath, "Storage");
                    file.Save(file1, severPath, "Storage");
                    item.LinkImg1 = "/Image/Storage/" + file1.FileName;
                }
                if (file2?.FileName != null)
                {
                    file.Delete(item.LinkImg2, severPath, "Storage");
                    file.Save(file2, severPath, "Storage");
                    item.LinkImg2 = "/Image/Storage/" + file2.FileName;
                }
                if (file3?.FileName != null)
                {
                    file.Delete(item.LinkImg3, severPath, "Storage");
                    file.Save(file3, severPath, "Storage");
                    item.LinkImg3 = "/Image/Storage/" + file3.FileName;
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
                var item = (from a in Db.OCungs where a.ID == id select a).SingleOrDefault();
                if (item == null) return false;

                var file = new Extend.FileAccess();
                var listFilename = item.LinkImg1.Split('/').ToList();
                file.Delete(listFilename[3], severPath, "Storage");

                listFilename = item.LinkImg2.Split('/').ToList();
                file.Delete(listFilename[3], severPath, "Storage");

                listFilename = item.LinkImg3.Split('/').ToList();
                file.Delete(listFilename[3], severPath, "Storage");

                Db.OCungs.DeleteOnSubmit(item);
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