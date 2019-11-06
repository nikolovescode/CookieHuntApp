using System;
using System.Collections.Generic;
using System.Text;

namespace CookieHuntApp.Models
{
    public class UserHasRole
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int StoreId { get; set; }
    }
}
