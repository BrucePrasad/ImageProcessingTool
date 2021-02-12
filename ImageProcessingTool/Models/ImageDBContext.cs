using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ImageProcessingTool.Models
{
    public class ImageDBContext : DbContext
    {
        public ImageDBContext() : base("name=sqlServerConnection")
        {
        }

        public DbSet<Image> Images { get; set; }                
        }
}