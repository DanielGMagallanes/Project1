using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace project_1.Security
{
    public class CanAccessStore : IAuthorizationRequirement
    {
    }
    public class CanAccessUserHandler : AuthorizationHandler<CanAccessStore, int>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CanAccessStore requirement, int user)
        {
            if (context.User.HasClaim(claim => claim.Type == "IsAdmin") || context.User.HasClaim("Id", user.ToString()))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
