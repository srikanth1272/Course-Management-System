using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseApp.Models
{
    public class Subject
    {
        public string SubjectId { get; set; }
        public string Title { get; set; }
        public int TotalClasses { get; set; }
        public int Credits { get; set; }
    }
}