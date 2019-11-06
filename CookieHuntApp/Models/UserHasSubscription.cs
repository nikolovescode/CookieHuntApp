using System;
using System.Collections.Generic;
using System.Text;

namespace CookieHuntApp.Models
{
    public class UserHasSubscription
    {
        public int Id { get; set; }
        public string UserHasRoleEmail { get; set; }
        public int StandardCategoryId { get; set; }
        public int StoreId { get; set; }
    }
}
