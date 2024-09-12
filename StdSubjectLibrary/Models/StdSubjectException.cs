using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StdSubjectLibrary.Models
{
    public class StdSubjectException:Exception
    {
        public StdSubjectException(string msg):base(msg) { }
    }
}
