using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EShopOnline_v2.DAL;

namespace EShopOnline_v2.Areas.Admin.DAL
{
    public class CardVgaDataAccessLayer
    {
        private static readonly DataClassesDataContext Db = new DataClassesDataContext();

        public static List<CardVGA> GetTable()
        {
            return (from a in Db.CardVGAs where a.ID != 0 select a).ToList();
        }

        public static bool Insert(CardVGA newProduct, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3, string severPath)
        {
            try
            {
                var file = new Extend.FileAccess();

                if (file.Save(file1, severPath, "VGA"))
                {
                    newProduct.LinkImg1 = "/Images/VGA/" + file1.FileName;
                }
                if (file.Save(file2, severPath, "VGA"))
                {
                    newProduct.LinkImg2 = "/Images/VGA/" + file2.FileName;
                }
                if (file.Save(file3, severPath, "VGA"))
                {
                    newProduct.LinkImg3 = "/Images/VGA/" + file3.FileName;
                }

                var list = (from a in Db.CardVGAs select a).ToList();

                newProduct.ID = list.Last().ID + 1;

                Db.CardVGAs.InsertOnSubmit(newProduct);
                Db.SubmitChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static CardVGA GetItem(int id)
        {
            return (from a in Db.CardVGAs where a.ID == id select a).SingleOrDefault();
        }

        public static bool Update(int id, CardVGA product, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3, string severPath)
        {
            try
            {
                var item = (from a in Db.CardVGAs where a.ID == id select a).SingleOrDefault();
                if (item == null) return false;

                item.BaoHanh        = product.BaoHanh;
                item.BangThong      = product.BangThong;
                item.BoNhoVGA       = product.BoNhoVGA;
                item.ChuanGiaoTiep  = product.ChuanGiaoTiep;
                item.CongGiaoTiep   = product.CongGiaoTiep;
                item.GPU            = product.GPU;
                item.HangSX         = product.HangSX;
                item.Gia            = product.Gia;
                item.GiamGia        = product.GiamGia;
                item.HangSX         = product.HangSX;
                item.XungNhip       = product.XungNhip;
                item.NamSX          = product.NamSX;
                item.SoLuong        = product.SoLuong;
                item.Ten            = product.Ten;
                item.XuatXu         = product.XuatXu;

                var file = new Extend.FileAccess();
                if (file1?.FileName != null)
                {
                    file.Delete(item.LinkImg1, severPath, "VGA");
                    file.Save(file1, severPath, "VGA");
                    item.LinkImg1 = "/Image/VGA/" + file1.FileName;
                }
                if (file2?.FileName != null)
                {
                    file.Delete(item.LinkImg2, severPath, "VGA");
                    file.Save(file2, severPath, "VGA");
                    item.LinkImg2 = "/Image/VGA/" + file2.FileName;
                }
                if (file3?.FileName != null)
                {
                    file.Delete(item.LinkImg3, severPath, "VGA");
                    file.Save(file3, severPath, "VGA");
                    item.LinkImg3 = "/Image/VGA/" + file3.FileName;
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
                var item = (from a in Db.CardVGAs where a.ID == id select a).SingleOrDefault();
                if (item == null) return false;

                var file = new Extend.FileAccess();
                var listFilename = item.LinkImg1.Split('/').ToList();
                file.Delete(listFilename[3], severPath, "VGA");

                listFilename = item.LinkImg2.Split('/').ToList();
                file.Delete(listFilename[3], severPath, "VGA");

                listFilename = item.LinkImg3.Split('/').ToList();
                file.Delete(listFilename[3], severPath, "VGA");

                Db.CardVGAs.DeleteOnSubmit(item);
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