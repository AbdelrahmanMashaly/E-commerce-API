using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.Entities.Identity;

namespace Talabat.Repository.identity
{
    public static class IdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "AbdelrahmanMashaly",
                    Email = "Abdo@gmail.com",
                    UserName = "Abdo.M",
                    PhoneNumber = "01010101"
                };
                await userManager.CreateAsync(user,"Pa$$w0rd");
            }
        }
    }
}
