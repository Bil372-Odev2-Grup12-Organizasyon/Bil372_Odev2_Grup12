using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Models
{
    public class AddRoleViewModel
    {
        [Required(ErrorMessage = "Ad alanı gereklidir.")]
        [Display(Name = "Rol Adı : ")]
        public string RoleName { get; set; }
    }
}
