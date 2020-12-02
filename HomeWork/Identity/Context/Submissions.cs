using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Context
{
    public class Submissions
    {

        public int SubmissionID { get; set; }

        public int prevSubmissionID { get; set; }

        public int ConfId { get; set; }
        public Conference Conference { get; set; }


    }
}
