using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CanIEatHere.Models
{
    public class ReviewUserViewModel
    {
        public Restaurant restaurant { get; set; }
        public AspNetUser user { get; set; }
    }
}