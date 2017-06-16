using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CanIEatHere.Models
{
    public class ReviewCreateViewModel
    {
        public Review review { get; set; }
        public FoodItem foodItem { get; set; }
    }
}