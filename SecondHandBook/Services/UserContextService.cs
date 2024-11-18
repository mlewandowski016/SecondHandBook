using AutoMapper;
using SecondHandBook.Entities;
using System.Security.Claims;

namespace SecondHandBook.Services
{
    public interface IUserContextService
    {
        ClaimsPrincipal User { get; }
        int? GetUserId { get; }
    }
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SecondHandBookDbContext _secondHandBookDbContext;
        private readonly IMapper _mapper;

        public UserContextService(IHttpContextAccessor httpContextAccessor, SecondHandBookDbContext secondHandBookDbContext, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _secondHandBookDbContext = secondHandBookDbContext;
            _mapper = mapper;
        }

        public ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User;

        public int? GetUserId => User is null ? null : int.Parse(User.FindFirst(u => u.Type == ClaimTypes.NameIdentifier).Value);
    }
}
