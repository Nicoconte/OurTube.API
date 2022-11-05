using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OurTube.API.Data;
using OurTube.API.Schemas.Types;

namespace OurTube.API.UseCases.Users.Queries
{
    public class ListUsersQuery : IRequest<List<UserType>>
    {
    }

    public class ListUsersQueryHandler : IRequestHandler<ListUsersQuery, List<UserType>>
    {
        private ApplicationDbContext _context;

        private IMapper _mapper;

        public ListUsersQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<UserType>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users.ProjectTo<UserType>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
