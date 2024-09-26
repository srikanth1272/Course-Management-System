using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseApp.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Remote("CheckEmail", "User", ErrorMessage = "Email already exists!")]
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

    }
}