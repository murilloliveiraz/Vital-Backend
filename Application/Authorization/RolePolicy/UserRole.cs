using Microsoft.AspNetCore.Authorization;

namespace Application.Authorization.RolePolicy
{
    public class UserRole : IAuthorizationRequirement
    {
        public string Role { get; set; }

        public UserRole(string role)
        {
            Role = role;
        }
    }
}
