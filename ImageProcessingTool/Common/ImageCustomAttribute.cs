using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ImageProcessingTool.Common
{

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ImageCustomAttribute : ValidationAttribute
    {
        public List<string> AllowedExtensions { get; set; } = new List<string> { "png", "jpg", "bmp" };

        public override bool IsValid( object value)
        {
            HttpPostedFileBase[] files = value as HttpPostedFileBase[];
            if (files == null) return false;
            bool isValid = true;
            var fileslist = files?.ToList();
            foreach (var file in fileslist)
            {
                if(!AllowedExtensions.Any(y => file.FileName.EndsWith(y, StringComparison.InvariantCultureIgnoreCase)))
                    return false;
            }
            //if (file != null)
            //{
            //    var fileName = file.FileName;
            //    isValid = AllowedExtensions.Any(y => fileName.EndsWith(y));
            //}
            return true;
        }
    }
}