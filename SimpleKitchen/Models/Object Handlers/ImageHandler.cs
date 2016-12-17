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
            if (file != null && file.ContentLength > 0)
            {
                string newFileName = Guid.NewGuid().ToString() + Path.GetFileName(file.FileName);
                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/UserImages"),
                                           newFileName);
                file.SaveAs(path);
                return ("/Content/UserImages/" + newFileName);
            }
            else return null;
        }

        public string SaveEditedImageAndGetReference(string oldImageReference, HttpPostedFileBase file)
        {
            if(!(Path.GetFileName(file.FileName) == oldImageReference.Substring(57))) {
                    string newFileName = Guid.NewGuid().ToString() + Path.GetFileName(file.FileName);
                    string path = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/UserImages"),
                                           newFileName);
                    file.SaveAs(path);
                    return ("/Content/UserImages/" + newFileName);
            }
            else return oldImageReference;
        }
    }
}