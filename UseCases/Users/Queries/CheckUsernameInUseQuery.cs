using MediatR;
using Microsoft.EntityFrameworkCore;
using OurTube.API.Data;

namespace OurTube.API.UseCases.Users.Queries
{
    public class CheckUsernameInUseQuery : IRequest<bool>
    {
        public String Username { get; set; }
    }

    public class CheckUsernameInUseQueryHandler : IRequestHandler<CheckUsernameInUseQuery, bool>
    {
        private ApplicationDbContext _context;

        public CheckUsernameInUseQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(CheckUsernameInUseQuery request, CancellationToken cancellationToken)
        {
            return (await _context.Users.FirstOrDefaultAsync(c => c.Username.ToLower() == request.Username.ToLower())) != null;
        }
    }
}
