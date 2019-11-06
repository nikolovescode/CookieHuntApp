using System;
using System.Collections.Generic;
using System.Text;

namespace CookieHuntApp.Models
{
    class PurchaseHistory
    {
        public int Id { get; set; }
        public string UserHasRoleEmail { get; set; }
        public int StoreId { get; set; }
        public int Points { get; set; }
    }
}
