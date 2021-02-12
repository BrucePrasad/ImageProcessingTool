using ImageProcessingTool.Models;
using System;
using System.Collections.Generic;

using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageProcessingTool.Shared;
using System.Drawing;

namespace ImageProcessingTool.Controllers
{
    public class ImageController : Controller
    {
        // GET: Image
        [HttpGet]
        public ActionResult Add()
        {
            List<Models.Image> images;
            using (ImageDBContext dbContext = new ImageDBContext())
            {
                images = dbContext.Images.Select(x => x).ToList();
            }

            ViewBag.AllImages = images ;
            Models.Image imageModel = new Models.Image();
            if (TempData["IsValidImage"] != null )
            {
                imageModel.IsValidImage = (bool)TempData["IsValidImage"];
                imageModel.Message = (string)TempData["Message"];
            }

            return View(imageModel);
        }

        [HttpGet]
        public ActionResult Filter(string file)
        {
            ImageFilter filter = new ImageFilter();
            using (ImageDBContext dbContext = new ImageDBContext())
            {
                var image = dbContext.Images.Where(x => x.ImagePath.Equals(file)).FirstOrDefault();
                if(image != null)
                {
                    filter.OriginalImage = image.ImagePath;

                    string filepath = Path.GetDirectoryName (image.ImagePath);
                    string filename = Path.GetFileNameWithoutExtension(image.ImagePath);
                    string extension = Path.GetExtension(image.ImagePath);

                    filter.GreyscaleImage = Path.Combine(filepath, filename + "_greyed" + extension);
                }                                                 
            }
            return View(filter);
        }


        [HttpPost]
        public ActionResult Add(Models.Image imageModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    //imageModel.UploadValidationMessage = imageModel.ImageFile.FileName + " is not a valid image file";
                    //imageModel.IsValidImage = false;
                    TempData.Add("Message", "please upload a valid image type");
                    TempData.Add("IsValidImage", false);
                    return RedirectToAction("Add");
                }
                foreach(var imagefile in imageModel.ImageFiles)
                {
                    string filename = Path.GetFileNameWithoutExtension(imagefile.FileName);
                    string extension = Path.GetExtension(imagefile.FileName);
                    string filepart = filename + "-" + DateTime.Now.ToString("yyyymmddHHmmss");                    
                    filename = filepart + extension;
                    string greyedfilename = filepart + "_greyed" + extension; ;
                    imageModel.ImagePath = "~/Images/" + filename;
                    imageModel.Title = filename;
                    string fullyqualifiedfname = Path.Combine(Server.MapPath("~/Images/"), filename);
                    imagefile.SaveAs(fullyqualifiedfname);
                  
                    Bitmap greyedBitmap = ImageCommon.GetGrayScaledImage(ImageCommon.GetBitMapFromHttpPostedFileBase(imagefile.InputStream));
                    greyedfilename = Path.Combine(Server.MapPath("~/Images/"), greyedfilename);
                    greyedBitmap.Save(greyedfilename);

                    using (ImageDBContext dbContext = new ImageDBContext())
                    {
                        dbContext.Images.Add(imageModel);
                        dbContext.SaveChanges();
                    }         
                }               
                TempData.Add("Message", "image(s) uploaded successfully");
                TempData.Add("IsValidImage", true);                
            }
            catch (Exception e)
            {
                
            }
            return RedirectToAction("Add");            
        }
        [HttpGet]
        public ActionResult ViewAll()
        {
            List<Models.Image> images;
            using (ImageDBContext dbContext = new ImageDBContext())
            {
                images =  dbContext.Images.Select(x => x).ToList();               
            }
            return View(images);
        }
    }
}