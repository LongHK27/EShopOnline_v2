using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShopOnline_v2.DAL
{
    public class Accounts
    {
        public static DataClassesDataContext Db = new DataClassesDataContext();
        public static int CheckLogIn(TaiKhoan taiKhoan)
        {
            var user =
                (from a in Db.TaiKhoans where a.Ten == taiKhoan.Ten && a.MatKhau == taiKhoan.MatKhau select a)
                    .SingleOrDefault();
            if (user != null)
            {
                return user.Loai == true ? 1 : 2;
            }
            else
            {
                return 0;
            }
        }
    }
}