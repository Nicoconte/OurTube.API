using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OurTube.API.Data;
using OurTube.API.Schemas.Types;
using System.Collections.Generic;

namespace OurTube.API.UseCases.Rooms.Queries
{
    public class GetBannedUsersFromRoomQuery : IRequest<List<ParticipantType>>
    {
        public String RoomId { get; set; }
    }

    public class GetBannedUsersFromRoomQueryHandler : IRequestHandler<GetBannedUsersFromRoomQuery, List<ParticipantType>>
    {
        private IMapper _mapper;
        private ApplicationDbContext _context;

        public GetBannedUsersFromRoomQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ParticipantType>> Handle(GetBannedUsersFromRoomQuery request, CancellationToken cancellationToken)
        {
            var bannedUsers = await _context
                .Participants
                .ProjectTo<ParticipantType>(_mapper.ConfigurationProvider)
                .AsQueryable()
                .Where(c => c.RoomId == request.RoomId && c.IsBanned).ToListAsync();

            return bannedUsers;
        }
    }
}
