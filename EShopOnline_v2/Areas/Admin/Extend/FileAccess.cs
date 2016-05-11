using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace EShopOnline_v2.Areas.Admin.Extend
{
    public class FileAccess
    {
        public bool Save(HttpPostedFileBase file, string severPath, string folder)
        {
            try
            {
                if (file.FileName == null) return false;
                var pathFile = severPath + "\\Images\\" + folder;
                pathFile = Path.Combine(pathFile, Path.GetFileName(file.FileName));
                file.SaveAs(pathFile);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool Delete(string fileName, string severPath, string folder)
        {
            try
            {
                var pathFile = severPath + "\\Images\\" + folder;
                pathFile = Path.Combine(pathFile, fileName);
                if(!File.Exists(pathFile)) return false;
                var file = new FileInfo(pathFile);
                file.Delete();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}