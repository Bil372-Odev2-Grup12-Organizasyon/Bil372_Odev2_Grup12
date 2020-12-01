using Identity.Context;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.CustomValidator
{
    public class CustomPasswordValidator : IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
        {
            List<IdentityError> IdentityErrors = new List<IdentityError>();
            if (password.ToLower().Contains(user.UserName.ToLower()))
            {
                IdentityErrors.Add(new IdentityError()
                {
                    Code = "PasswordContainsUsername",
                    Description = "Parola Kullanıcı Adı İçermemelidir."
                });


            }

            if (IdentityErrors.Count > 0)
            {
                return Task.FromResult(IdentityResult.Failed(IdentityErrors.ToArray()));
            }
            else
            {
                return Task.FromResult(IdentityResult.Success);
            }
        }
    }
}
