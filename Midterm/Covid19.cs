using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midterm
{
    public class Covid19
    {
        public string Country { get; set; }
        public string State { get; set; }
        public int NumberofCase { get; set; }
        public string ConfirmedDate { get; set; }


        public List<Covid19> covid19List { get; set; }

    }
}

