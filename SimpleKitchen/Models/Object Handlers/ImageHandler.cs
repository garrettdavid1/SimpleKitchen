using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SimpleKitchen.Models.Object_Handlers
{
    public class ImageHandler
    {
        public string SaveInitialImageAndGetReference(HttpPostedFileBase file)
        { 
            string FileName = RemoveSpaces(Path.GetFileName(file.FileName));
            string newFileName = ConstructNewFileName(FileName);
            string path = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/UserImages"),
                                       newFileName);
            file.SaveAs(path);
            return ("/Content/UserImages/" + newFileName);
        }

        public string SaveEditedImageAndGetReference(string oldImageReference, HttpPostedFileBase file)
        {
            string FileName = RemoveSpaces(Path.GetFileName(file.FileName));
            if(!(FileName == oldImageReference.Substring(57))) {
                    string newFileName = ConstructNewFileName(FileName);
                    string path = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/UserImages"),
                                           newFileName);
                    file.SaveAs(path);
                    return ("/Content/UserImages/" + newFileName);
            }
            else return oldImageReference;
        }

        public string RemoveSpaces(string fileName)
        {
            return fileName.Replace(' ', '-');
        }

        public string ConstructNewFileName(string oldFileName)
        {
            return (Guid.NewGuid() + "-" + oldFileName);
        }
    }
}