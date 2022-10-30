using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OurTube.API.Data;
using OurTube.API.Schemas.Types;

namespace OurTube.API.UseCases.Rooms.Queries
{
    public class GetParticipantFromRoomQuery : IRequest<ParticipantType>
    {
        public String Username { get; set; }
        public String RoomId { get; set; }
    }

    public class GetParticipantFromRoomQueryHandler : IRequestHandler<GetParticipantFromRoomQuery, ParticipantType>
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public GetParticipantFromRoomQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ParticipantType> Handle(GetParticipantFromRoomQuery request, CancellationToken cancellationToken)
        {
            var participant = await _context
                .Participants
                .ProjectTo<ParticipantType>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(p => p.User.Username == request.Username && p.RoomId == request.RoomId);

            if (participant == null) return null;

            return participant;
        }
    }
}
