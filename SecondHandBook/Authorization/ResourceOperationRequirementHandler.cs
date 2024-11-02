using Microsoft.AspNetCore.Authorization;
using SecondHandBook.Entities;
using System.Security.Claims;

namespace SecondHandBook.Authorization
{
    public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, BookOffer>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, BookOffer display)
        {
            if (requirement.ResourceOperation == ResourceOperation.Read || requirement.ResourceOperation == ResourceOperation.Create)
                context.Succeed(requirement);

            var userId = int.Parse(context.User.FindFirst(u => u.Type == ClaimTypes.NameIdentifier).Value);

            if (display.GiverId == userId && (requirement.ResourceOperation == ResourceOperation.Update || requirement.ResourceOperation == ResourceOperation.Delete))
                context.Succeed(requirement);

            if (display.TakerId == userId && requirement.ResourceOperation == ResourceOperation.Collect)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
// to jest do autoryzowania usuwania i modyfikacji zasobów display
