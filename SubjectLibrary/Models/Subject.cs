using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectLibrary.Models
{
    public class Subject
    {
        public string SubjectId { get; set; }
        public string Title { get; set; }
        public int TotalClasses { get; set; }
        public int Credits { get; set; }
    }
}
