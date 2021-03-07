using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class Wind
    {
        
            public double speed { get; set; }
            public int deg { get; set; }
            //public Speed speed { get; set; }
            public string gusts { get; set; }
            public Direction direction { get; set; }
       
    }
}