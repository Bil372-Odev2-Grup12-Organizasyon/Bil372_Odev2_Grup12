using Identity.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Models
{
    public class AddTagViewModel
    {
        public Conference Conference { get; set; }

        [Required(ErrorMessage = "Konferans tag alanı zorunludur!")]
        public string ConferenceTag { get; set; }
    }

    public class AddSubmissionViewModel
    {
        public Conference Conference { get; set; }

        [Required(ErrorMessage = "Sunum alanı zorunludur!")]
        public string Submission { get; set; }

        [Required(ErrorMessage = "Sunum sırası zorunludur!")]
        [Range(1, int.MaxValue, ErrorMessage = "Geçerli değer giriniz.")]
        public int prevSubmission { get; set; }
    }
}
