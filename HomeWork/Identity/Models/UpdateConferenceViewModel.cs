using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Models
{
    public class UpdateConferenceViewModel
    {
        public int ConfID { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public DateTime CreateionDateTime { get; set; }
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public string ShortName { get; set; }
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public int Year { get; set; }
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public DateTime SubmissionDeadline { get; set; }
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public string WebSite { get; set; }
        public int CreateUserId { get; set; }
    }
}
