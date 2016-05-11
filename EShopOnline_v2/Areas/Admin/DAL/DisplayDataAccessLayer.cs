using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EShopOnline_v2.DAL;

namespace EShopOnline_v2.Areas.Admin.DAL
{
    public class DisplayDataAccessLayer
    {
        private static readonly DataClassesDataContext Db = new DataClassesDataContext();

        public static List<ManHinh> GetTable()
        {
            return (from a in Db.ManHinhs where a.ID != 0 select a).ToList();
        }

        public static bool Insert(ManHinh newProduct, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3, string severPath)
        {
            try
            {
                var file = new Extend.FileAccess();

                if (file.Save(file1, severPath, "Display"))
                {
                    newProduct.LinkImg1 = "/Images/Display/" + file1.FileName;
                }
                if (file.Save(file2, severPath, "Display"))
                {
                    newProduct.LinkImg2 = "/Images/Display/" + file2.FileName;
                }
                if (file.Save(file3, severPath, "Display"))
                {
                    newProduct.LinkImg3 = "/Images/Display/" + file3.FileName;
                }

                var list = (from a in Db.ManHinhs select a).ToList();

                newProduct.ID = list.Last().ID + 1;

                Db.ManHinhs.InsertOnSubmit(newProduct);
                Db.SubmitChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static ManHinh GetItem(int id)
        {
            return (from a in Db.ManHinhs where a.ID == id select a).SingleOrDefault();
        }

        public static bool Update(int id, ManHinh product, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3, string severPath)
        {
            try
            {
                var item = (from a in Db.ManHinhs where a.ID == id select a).SingleOrDefault();
                if (item == null) return false;

                item.BaoHanh    = product.BaoHanh;
                item.DoPhanGiai = product.DoPhanGiai;
                item.KichThuoc  = product.KichThuoc;
                item.Gia        = product.Gia;
                item.GiamGia    = product.GiamGia;
                item.HangSX     = product.HangSX;
                item.Loai       = product.Loai;
                item.NamSX      = product.NamSX;
                item.SoLuong    = product.SoLuong;
                item.Ten        = product.Ten;
                item.XuatXu     = product.XuatXu;
                
                var file = new Extend.FileAccess();
                if (file1?.FileName != null)
                {
                    file.Delete(item.LinkImg1, severPath, "Display");
                    file.Save(file1, severPath, "CPDisplayU");
                    item.LinkImg1 = "/Image/CDisplayPU/" + file1.FileName;
                }
                if (file2?.FileName != null)
                {
                    file.Delete(item.LinkImg2, severPath, "Display");
                    file.Save(file2, severPath, "Display");
                    item.LinkImg2 = "/Image/Display/" + file2.FileName;
                }
                if (file3?.FileName != null)
                {
                    file.Delete(item.LinkImg3, severPath, "Display");
                    file.Save(file3, severPath, "Display");
                    item.LinkImg3 = "/Image/Display/" + file3.FileName;
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
                var item = (from a in Db.ManHinhs where a.ID == id select a).SingleOrDefault();
                if (item == null) return false;

                var file = new Extend.FileAccess();
                var listFilename = item.LinkImg1.Split('/').ToList();
                file.Delete(listFilename[3], severPath, "Display");

                listFilename = item.LinkImg2.Split('/').ToList();
                file.Delete(listFilename[3], severPath, "Display");

                listFilename = item.LinkImg3.Split('/').ToList();
                file.Delete(listFilename[3], severPath, "Display");

                Db.ManHinhs.DeleteOnSubmit(item);
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