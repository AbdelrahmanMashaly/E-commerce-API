using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Domain.Entities.Identity;

namespace Talabat.APIs.Extensions
{
    public static class UserManagerExtension
    {
        public static async Task<AppUser?> GetUserAddressAsync(this UserManager<AppUser> userManager, ClaimsPrincipal user)
        {
             var Email = user.FindFirstValue(ClaimTypes.Email);

            var User = await userManager.Users.Include(U=>U.address).FirstOrDefaultAsync(U=>U.Email == Email);

            return User;
        }
    }
}
