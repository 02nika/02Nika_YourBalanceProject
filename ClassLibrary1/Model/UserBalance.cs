using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class UserBalance
    {
        public decimal Balance { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
    }
}