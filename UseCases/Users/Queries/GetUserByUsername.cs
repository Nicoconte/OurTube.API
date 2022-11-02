using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OurTube.API.Data;
using OurTube.API.Schemas.Types;

namespace OurTube.API.UseCases.Users.Queries
{
    public class GetUserByUsernameQuery : IRequest<UserType>
    {
        public string Username { get; set; }
    }

    public class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQuery, UserType>
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public GetUserByUsernameQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserType> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            var username = request.Username;

            var user = await _context.Users.ProjectTo<UserType>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(c => c.Username == username);

            return user;
        }
    }
}
