using System;
using System.Collections.Generic;
using System.Text;

namespace CookieHuntApp.Models
{
    public class GroupBCategory
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int StandardCategoryId { get; set; }
        public int Points { get; set; }
        public int PercentOff { get; set; }
    }
}
