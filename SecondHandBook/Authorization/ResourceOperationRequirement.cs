using Microsoft.AspNetCore.Authorization;

namespace SecondHandBook.Authorization
{
    public enum ResourceOperation
    {
        Create,
        Read,
        Update,
        Delete,
        Reserve,
        Collect
    }
    public class ResourceOperationRequirement : IAuthorizationRequirement
    {
        public ResourceOperationRequirement(ResourceOperation resourceOperation)
        {
            ResourceOperation = resourceOperation;
        }
        public ResourceOperation ResourceOperation { get; }
    }
}
