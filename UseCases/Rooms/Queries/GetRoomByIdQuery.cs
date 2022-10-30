using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OurTube.API.Data;
using OurTube.API.Entities;
using OurTube.API.Schemas.Types;

namespace OurTube.API.UseCases.Rooms.Queries
{
    public class GetRoomByIdQuery : IRequest<RoomType>
    {
        public String RoomId { get; set; }
    }

    public class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery, RoomType>
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public GetRoomByIdQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RoomType> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
        {
            var room = await _context
                .Rooms
                .ProjectTo<RoomType>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(r => r.Id == request.RoomId);

            return room;
        }
    }
}
