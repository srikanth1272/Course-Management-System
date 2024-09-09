using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectLibrary.Models
{
    public class SubjectException:Exception
    {
        public SubjectException(string msg):base(msg) { }
    }
}
