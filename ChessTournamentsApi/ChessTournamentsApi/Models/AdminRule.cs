using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ChessTournamentsApi.Models
{
    public class AdminRule : AuthorizationHandler<AdminRule>, IAuthorizationRequirement
    {

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRule requirement)
        {
            if (context.User.HasClaim("scope","scope.fullaccess"))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
           return  Task.CompletedTask;
        }
    }
}

