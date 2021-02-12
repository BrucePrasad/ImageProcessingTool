using ImageProcessingTool.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ImageProcessingTool.Models
{
    [Table("Images")]
    public class Image
    {
        [Key]
        public int ImageID { get; set; }
        public string Title { get; set; }
        [DisplayName("Upload File")]
        public string ImagePath { get; set; }
        [NotMapped()]
        [Required]
        [Display(Name = "File types .png | .jpg | .bmp")]
        [ImageCustomAttribute(/*AllowedExtensions = = new List<string> { ".jpg", ".bmp" }, */
            ErrorMessage = "select only image files .bmp || .png ||| .jpg")]
        public HttpPostedFileBase[] ImageFiles { get; set; }
        [NotMapped()]
        public string Message { get; set; }
        [NotMapped()]
        public bool IsValidImage;
    }
}