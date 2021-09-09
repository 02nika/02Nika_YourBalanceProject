using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class RegistrationInfo
    {
        public string UserName { get; set; }
        public string Password  { get; set; }
        public string RepeatPassword  { get; set; }
        public string Balance  { get; set; }
        public string Desc  { get; set; }
    }
}