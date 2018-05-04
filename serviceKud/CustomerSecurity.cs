using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CustomerServices;

namespace serviceKud
{
    public class CustomerSecurity
    {
        public static bool Login(string username, string password)
        {
            using (CustomerDBEntities entites = new CustomerDBEntities())
            {
                return entites.Users.Any(user => user.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && user.Password == password);
            }
        }
    }
}