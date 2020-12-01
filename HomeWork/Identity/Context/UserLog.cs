using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Context
{
    public class UserLog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Primary_Email { get; set; }
        public string Secondary_Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }

        public string UrlAddress { get; set; }

        public string City { get; set; }
        public string Country { get; set; }
        public DateTime CreateionDate { get; set; }
    }
}
