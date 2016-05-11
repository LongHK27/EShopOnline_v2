using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EShopOnline_v2.DAL;

namespace EShopOnline_v2.Areas.Admin.DAL
{
    public class CpuDataAccessLayer
    {
        private static readonly DataClassesDataContext Db = new DataClassesDataContext();

        public static List<CPU> GetTable()
        {
            return (from a in Db.CPUs where a.ID != 0 select a).ToList();
        }

        public static bool Insert(CPU newProduct, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3, string severPath)
        {
            try
            {
                var file = new Extend.FileAccess();

                if (file.Save(file1, severPath, "CPU"))
                {
                    newProduct.LinkImg1 = "/Images/CPU/" + file1.FileName;
                }
                if (file.Save(file2, severPath, "CPU"))
                {
                    newProduct.LinkImg2 = "/Images/CPU/" + file2.FileName;
                }
                if (file.Save(file3, severPath, "CPU"))
                {
                    newProduct.LinkImg3 = "/Images/CPU/" + file3.FileName;
                }

                var list = (from a in Db.CPUs select a).ToList();

                newProduct.ID = list.Last().ID + 1;

                Db.CPUs.InsertOnSubmit(newProduct);
                Db.SubmitChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static CPU GetItem(int id)
        {
            return (from a in Db.CPUs where a.ID == id select a).SingleOrDefault();
        }

        public static bool Update(int id, CPU product, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3, string severPath)
        {
            try
            {
                var item = (from a in Db.CPUs where a.ID == id select a).SingleOrDefault();
                if (item == null) return false;

                item.BaoHanh = product.BaoHanh;
                item.BoNhoDem = product.BoNhoDem;
                item.CongNghe = product.CongNghe;
                item.CongSuat = product.CongSuat;
                item.Gia = product.Gia;
                item.GiamGia = product.GiamGia;
                item.HangSX = product.HangSX;
                item.Loai = product.Loai;
                item.NamSX = product.NamSX;
                item.SoLuong = product.SoLuong;
                item.Ten = product.Ten;
                item.XuatXu = product.XuatXu;
                item.SoNhan = product.SoNhan;
                item.Socket = product.Socket;
                item.TocDo = product.TocDo;

                var file = new Extend.FileAccess();
                if (file1?.FileName != null)
                {
                    file.Delete(item.LinkImg1, severPath, "CPU");
                    file.Save(file1, severPath, "CPU");
                    item.LinkImg1 = "/Image/CPU/" + file1.FileName;
                }
                if (file2?.FileName != null)
                {
                    file.Delete(item.LinkImg2, severPath, "CPU");
                    file.Save(file2, severPath, "CPU");
                    item.LinkImg2 = "/Image/CPU/" + file2.FileName;
                }
                if (file3?.FileName != null)
                {
                    file.Delete(item.LinkImg3, severPath, "CPU");
                    file.Save(file3, severPath, "CPU");
                    item.LinkImg3 = "/Image/CPU/" + file3.FileName;
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
                var item = (from a in Db.CPUs where a.ID == id select a).SingleOrDefault();
                if (item == null) return false;

                var file = new Extend.FileAccess();
                var listFilename = item.LinkImg1.Split('/').ToList();
                file.Delete(listFilename[3], severPath, "CPU");

                listFilename = item.LinkImg2.Split('/').ToList();
                file.Delete(listFilename[3], severPath, "CPU");

                listFilename = item.LinkImg3.Split('/').ToList();
                file.Delete(listFilename[3], severPath, "CPU");

                Db.CPUs.DeleteOnSubmit(item);
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