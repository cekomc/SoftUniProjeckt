using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoftUniProject.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime AddedOnDate { get; set; }

        public decimal Price { get; set; }

        public string Review { get; set; }

        //public HttpPostedFileBase UploadedFile { get; set; }



    }
}