using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CourseApp.Models
{
    public class StdSubject
    {
        public string RollNo { get; set; }
        public string SubjectId { get; set; }
        public int Semister { get; set; }
      
    }
}