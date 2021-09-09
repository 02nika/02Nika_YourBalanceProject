using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Image
    {
        [DataType(DataType.Upload)]
        [DisplayName("Upload Profile Picture")]
        public string ImageFile { get; set; }
    }
}