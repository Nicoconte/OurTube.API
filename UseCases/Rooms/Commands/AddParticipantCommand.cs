using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OurTube.API.Data;
using OurTube.API.Entities;
using OurTube.API.Extensions;
using OurTube.API.Helpers;
using OurTube.API.Schemas.Types;

namespace OurTube.API.UseCases.Rooms.Commands
{
    public class AddParticipantCommand : IRequest<ParticipantType>
    {
        public String RoomId { get; set; }
        public String RoomPassword { get; set; }
        public String ParticipantUsername { get; set; }
        public String RoomOwnerID { get; set; }
    }

    public class AddParticipantCommandHandler : IRequestHandler<AddParticipantCommand, ParticipantType>
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public AddParticipantCommandHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ParticipantType> Handle(AddParticipantCommand request, CancellationToken cancellationToken)
        {
            if (!await _context.Users.CheckIf(r => r.Username == request.ParticipantUsername))
            {
                throw new GraphQLException(new Error($"User {request.ParticipantUsername} doesnt exist."));
            }

            if (!await _context.Rooms.CheckIf(r => r.Id == request.RoomId))
            {
                throw new GraphQLException(new Error($"Room {request.RoomId} doesnt exist."));
            }

            var room = await _context.Rooms.FirstOrDefaultAsync(c => c.Id == request.RoomId);

            if (room.Owner.Id != request.RoomOwnerID)
            {
                throw new GraphQLException(new Error("You're not the owner of this room"));
            }

            if (room.Owner.Username == request.ParticipantUsername)
            {
                throw new GraphQLException(new Error("You are the owner of this room. You cant add yourself"));
            }

            if (room.Participants.Contains(_context.Participants.FirstOrDefault(c => c.User.Username == request.ParticipantUsername)))
            {
                throw new GraphQLException(new Error("This user is already in the room."));
            }

            if (!String.IsNullOrEmpty(request.RoomPassword) && (!room.Configuration.IsPublic && room.Configuration.Password == PasswordHelper.Hash(request.RoomPassword)))
            {
                throw new GraphQLException(new Error("Incorrect password"));
            }

            var user = await _context.Users.FirstOrDefaultAsync(c => c.Username == request.ParticipantUsername);

            var newParticipant = new Participant()
            {
                Id = Guid.NewGuid().ToString(),
                UserId = user.Id,
                RoomId = request.RoomId,
                IsBanned = false,
            };

            _context.Participants.Add(newParticipant);

            await _context.SaveChangesAsync();

            var obj = _mapper.Map<ParticipantType>(newParticipant);

            obj.User = _mapper.Map<UserType>(user);

            return obj;
        }
    }
}
