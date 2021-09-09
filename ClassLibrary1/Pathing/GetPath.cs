using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ClassLibrary1.Pathing
{
    public static class GetPath
    {
        public static string ProfileImage(HttpPostedFileBase imageName)
        {
            string fileName = Path.GetFileNameWithoutExtension(imageName.FileName);
            string extension = Path.GetExtension(imageName.FileName);
            fileName = fileName + DateTime.Now.ToString("yyyyMMssff") + extension;

            return fileName;
        }
    }
}
