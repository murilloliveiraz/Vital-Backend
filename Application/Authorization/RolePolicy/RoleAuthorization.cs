using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Application.Authorization.RolePolicy
{
    public class RoleAuthorization : AuthorizationHandler<UserRole>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserRole requirement)
        {
            var claims = context.User.Claims.ToList();
            Console.WriteLine($"Number of claims: {claims.Count}");
            foreach (var claim in claims)
            {
                Console.WriteLine($"Claim type: {claim.Type}, value: {claim.Value}");
            }
            var role = context.User.Claims;
            var userRoleClaim = context.User.FindFirst(claim => claim.Type == ClaimTypes.Role);

            if (userRoleClaim == null || userRoleClaim.Value != requirement.Role)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
