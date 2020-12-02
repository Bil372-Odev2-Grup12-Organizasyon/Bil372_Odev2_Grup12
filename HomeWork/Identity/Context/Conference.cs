using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Context
{
    public class Conference
    {
        public int ConfID { get; set; }
        public DateTime CreateionDateTime { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int Year { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime SubmissionDeadline { get; set; }
        public string WebSite { get; set; }


        public int CreateUserId { get; set; }
        public AppUser AppUser { get; set; }

        public List<ConferenceTags> ConferenceTags { get; set; }
        public List<Submissions> Submissions { get; set; }
    }
}
