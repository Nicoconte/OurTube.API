using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OurTube.API.Data;
using OurTube.API.Schemas.Types;
using System.Security.Claims;

namespace OurTube.API.UseCases.Users.Queries
{
    public class GetUserByIdQuery : IRequest<UserType>
    {
        public string Id { get; set; }
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserType>
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public GetUserByIdQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserType> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var id = request.Id;

            var user = await _context.Users.ProjectTo<UserType>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(c => c.Id == id);

            return user;
        }
    }
}
