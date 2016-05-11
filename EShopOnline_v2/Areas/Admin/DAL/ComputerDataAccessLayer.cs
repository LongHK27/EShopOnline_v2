using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EShopOnline_v2.DAL;
using EShopOnline_v2.Models;

namespace EShopOnline_v2.Areas.Admin.DAL
{
    public class ComputerDataAccessLayer
    {
        private static readonly DataClassesDataContext Db = new DataClassesDataContext();

        public static List<ComputerModel> GetTable()
        {
            var computer = (from a in Db.MayTinhs
                where a.ID != 0
                join b in Db.CPUs on a.CPU equals b.ID
                join c in Db.RAMs on a.RAM equals c.ID
                join d in Db.OCungs on a.OCung equals d.ID
                join e in Db.CardVGAs on a.CardVGA equals e.ID
                select new {a, b, c, d, e}).ToList();

            var listComputer = computer.Select(
                item => new ComputerModel(
                                item.a.ID, item.a.Ten, item.a.HangSX, item.a.Loai, 
                                item.b, item.c, item.d, item.e, 
                                item.a.HDH, item.a.Chipset, item.a.OQuang, item.a.CongGiaoTiep, item.a.LAN, 
                                Int32.Parse(item.a.SoLuong.ToString()), 
                                item.a.NamSX, 
                                Int32.Parse(item.a.BaoHanh.ToString()), 
                                Int32.Parse(item.a.Gia.ToString()), 
                                Int32.Parse(item.a.GiamGia.ToString()), 
                                item.a.LinkImg1, item.a.LinkImg2, item.a.LinkImg3)
                            ).ToList();

            return listComputer;
        }

        public static bool Insert(ComputerModel newProduct, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3, string severPath)
        {
            try
            {
                var file = new Extend.FileAccess();

                if (file.Save(file1, severPath, "Computer"))
                {
                    newProduct.LinkImg1 = "/Images/Computer/" + file1.FileName;
                }
                if (file.Save(file2, severPath, "Computer"))
                {
                    newProduct.LinkImg2 = "/Images/Computer/" + file2.FileName;
                }
                if (file.Save(file3, severPath, "Computer"))
                {
                    newProduct.LinkImg3 = "/Images/Computer/" + file3.FileName;
                }

                var list = (from a in Db.MayTinhs select a).ToList();

                var computer = new MayTinh
                {
                    ID              = list.Last().ID + 1,
                    Ten             = newProduct.Ten,
                    HangSX          = newProduct.HangSx,
                    Loai            = newProduct.Loai,
                    CPU             = newProduct.Cpu.ID,
                    RAM             = newProduct.Ram.ID,
                    OCung           = newProduct.Ocung.ID,
                    CardVGA         = newProduct.CardVga.ID,
                    HDH             = newProduct.Hdh,
                    Chipset         = newProduct.Chipset,
                    OQuang          = newProduct.Oquang,
                    CongGiaoTiep    = newProduct.CongGiaoTiep,
                    LAN             = newProduct.Lan,
                    SoLuong         = newProduct.SoLuong,
                    NamSX           = newProduct.NamSx,
                    BaoHanh         = newProduct.BaoHanh,
                    Gia             = newProduct.Gia,
                    GiamGia         = newProduct.GiamGia,
                    LinkImg1        = newProduct.LinkImg1,
                    LinkImg2        = newProduct.LinkImg2,
                    LinkImg3        = newProduct.LinkImg3
                };

                Db.MayTinhs.InsertOnSubmit(computer);
                Db.SubmitChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static ComputerModel GetItem(int id)
        {
            var com = (from a in Db.MayTinhs
                            where a.ID == id
                            join b in Db.CPUs on a.CPU equals b.ID
                            join c in Db.RAMs on a.RAM equals c.ID
                            join d in Db.OCungs on a.OCung equals d.ID
                            join e in Db.CardVGAs on a.CardVGA equals e.ID
                            select new { a, b, c, d, e }).SingleOrDefault();
            if (com == null) return null;
            var computer =  new ComputerModel(
                                com.a.ID, com.a.Ten, com.a.HangSX, com.a.Loai,
                                com.b, com.c, com.d, com.e,
                                com.a.HDH, com.a.Chipset, com.a.OQuang, com.a.CongGiaoTiep, com.a.LAN,
                                Int32.Parse(com.a.SoLuong.ToString()),
                                com.a.NamSX,
                                Int32.Parse(com.a.BaoHanh.ToString()),
                                Int32.Parse(com.a.Gia.ToString()),
                                Int32.Parse(com.a.GiamGia.ToString()),
                                com.a.LinkImg1, com.a.LinkImg2, com.a.LinkImg3)
                            ;

            return computer;
        }

        public static bool Update(int id, ComputerModel product, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3, string severPath)
        {
            try
            {
                var item = (from a in Db.MayTinhs where a.ID == id select a).SingleOrDefault();
                if (item == null) return false;

                item.Ten            = product.Ten;
                item.HangSX         = product.HangSx;
                item.Loai           = product.Loai;
                item.CPU            = product.Cpu.ID;
                item.RAM            = product.Ram.ID;
                item.OCung          = product.Ocung.ID;
                item.CardVGA        = product.CardVga.ID;
                item.HDH            = product.Hdh;
                item.Chipset        = product.Chipset;
                item.OQuang         = product.Oquang;
                item.CongGiaoTiep   = product.CongGiaoTiep;
                item.LAN            = product.Lan;
                item.SoLuong        = product.SoLuong;
                item.NamSX          = product.NamSx;
                item.BaoHanh        = product.BaoHanh;
                item.Gia            = product.Gia;
                item.GiamGia        = product.GiamGia;

                var file = new Extend.FileAccess();
                if (file1?.FileName != null)
                {
                    file.Delete(item.LinkImg1, severPath, "Computer");
                    file.Save(file1, severPath, "Computer");
                    item.LinkImg1 = "/Image/Computer/" + file1.FileName;
                }
                if (file2?.FileName != null)
                {
                    file.Delete(item.LinkImg2, severPath, "Computer");
                    file.Save(file2, severPath, "Computer");
                    item.LinkImg2 = "/Image/Computer/" + file2.FileName;
                }
                if (file3?.FileName != null)
                {
                    file.Delete(item.LinkImg3, severPath, "Computer");
                    file.Save(file3, severPath, "Computer");
                    item.LinkImg3 = "/Image/Computer/" + file3.FileName;
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
                var item = (from a in Db.MayTinhs where a.ID == id select a).SingleOrDefault();
                if (item == null) return false;

                var file = new Extend.FileAccess();
                var listFilename = item.LinkImg1.Split('/').ToList();
                file.Delete(listFilename[3], severPath, "Computer");

                listFilename = item.LinkImg2.Split('/').ToList();
                file.Delete(listFilename[3], severPath, "Computer");

                listFilename = item.LinkImg3.Split('/').ToList();
                file.Delete(listFilename[3], severPath, "Computer");

                Db.MayTinhs.DeleteOnSubmit(item);
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