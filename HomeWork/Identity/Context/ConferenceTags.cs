using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Context
{
    public class ConferenceTags
    {
        public int Id { get; set; }
        public string Tags { get; set; }

        public int ConfID { get; set; }
        public Conference Conference { get; set; }
    }
}
