﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CookieHuntApp.Models
{
    public class Coupon
    {
        public int Id { get; set; }
        public DateTime LastDate { get; set; }
        public int ItemId { get; set; }
        public int StandardCategoryId { get; set; }
        public int StoreId { get; set; }
        public float Price { get; set; }
        public int PointsWorth { get; set; }
    }
}
