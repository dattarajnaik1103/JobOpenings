using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobOpenings.Models
{
    public class PageFilter
    {
         public string Q { get; set; }
         public int PAGENO { get; set; }
         public int PAGESIZE { get; set; }
         public int? LOCATIONID { get; set; }
         public int? DEPARTMENTID { get; set; }

    }
}