using System;
using System.Security.Authentication;
using System.Security.Claims;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ClaimsPrincipleExtnesions
{
    public static async Task<AppUser> GetUserByEmail(this UserManager<AppUser> userManager, ClaimsPrincipal user)
    {
        var userToReturn = await userManager.Users.FirstOrDefaultAsync(u => u.Email == user.GetEmail());

        if (userToReturn == null) throw new AuthenticationException("User Not Found");

        return userToReturn;
    }

    public static async Task<AppUser> GetUserByEmailWithAddress(this UserManager<AppUser> userManager, ClaimsPrincipal user)
    {
        var userToReturn = await userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == user.GetEmail());

        if (userToReturn == null) throw new AuthenticationException("User Not Found");

        return userToReturn;
    }
    public static string GetEmail(this ClaimsPrincipal user)
    {
        var email = user.FindFirstValue(ClaimTypes.Email)
                    ?? throw new AuthenticationException("Email Claim Not Found");
        return email;
    }


}
