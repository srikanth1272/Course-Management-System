﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseApp.Models
{
    public class Student
    {
        public string RollNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DOB { get; set; }
        public string Address { get; set; }
    }
}