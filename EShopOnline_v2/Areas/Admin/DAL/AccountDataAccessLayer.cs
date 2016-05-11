using System;
using System.Collections.Generic;
using System.Linq;
using EShopOnline_v2.DAL;

namespace EShopOnline_v2.Areas.Admin.DAL
{
    public class AccountDataAccessLayer
    {
        public static readonly DataClassesDataContext Db = new DataClassesDataContext();

        public static List<TaiKhoan> GetTable()
        {
            return (from a in Db.TaiKhoans where a.ID != 0 select a).ToList();
        }

        public static TaiKhoan GetItem(int id)
        {
            return (from a in Db.TaiKhoans where a.ID == id select a).SingleOrDefault();
        }

        public static bool Insert(TaiKhoan newAccount)
        {
            try
            {
                var list = (from a in Db.TaiKhoans where a.ID != 0 select a).ToList();

                newAccount.ID = list.Last().ID + 1;
                newAccount.Loai = true;
                newAccount.NgayLap = DateTime.Now;

                Db.TaiKhoans.InsertOnSubmit(newAccount);
                Db.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool Update(int id, TaiKhoan account)
        {
            try
            {
                var item = (from a in Db.TaiKhoans where a.ID == id select a).SingleOrDefault();
                if (item == null) return false;

                item.DiaChi     = account.DiaChi;
                item.Email      = account.Email;
                item.HoTen      = account.HoTen;
                item.Loai       = account.Loai;
                item.MatKhau    = account.MatKhau;
                item.SDT        = account.SDT;
                item.Ten        = account.Ten;

                Db.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool Delete(int id)
        {
            try
            {
                var item = (from a in Db.TaiKhoans where a.ID == id select a).SingleOrDefault();
                if (item == null) return false;
                Db.TaiKhoans.DeleteOnSubmit(item);
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