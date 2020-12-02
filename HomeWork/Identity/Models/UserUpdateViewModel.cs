using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Models
{
    public class UserUpdateViewModel
    {
        [Display(Name = "Email : ")]
        [Required(ErrorMessage = "Email Alanı Zorunludur.")]
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir email adres giriniz.")]
        public string Email { get; set; }

        [Display(Name = "Telefon : ")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Name Alanı Zorunludur.")]
        [Display(Name = "İsim : ")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname Alanı Zorunludur.")]
        [Display(Name = "Soyisim : ")]
        public string Surname { get; set; }
        public IFormFile Picture { get; set; }

        public string PictureUrl { get; set; }
    }
}
