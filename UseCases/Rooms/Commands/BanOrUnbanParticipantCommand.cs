using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OurTube.API.Data;
using OurTube.API.Entities;
using OurTube.API.Schemas.Types;

namespace OurTube.API.UseCases.Rooms.Commands
{
    public enum ParticipantOperationTypes
    {
        Ban,
        Unban
    }

    public class BanOrUnbanParticipantCommand : IRequest<ParticipantType>
    {
        public ParticipantOperationTypes Operation { get; set; }
        public String ParticipantUsername { get; set; }
        public String RoomId { get; set; }
    }

    public class BanOrUnbanParticipantCommandHandler : IRequestHandler<BanOrUnbanParticipantCommand, ParticipantType>
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public BanOrUnbanParticipantCommandHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ParticipantType> Handle(BanOrUnbanParticipantCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var participant = await _context
                    .Participants
                    .FirstOrDefaultAsync(c => c.User.Username == request.ParticipantUsername && c.RoomId == request.RoomId);

                participant.IsBanned = request.Operation == ParticipantOperationTypes.Ban;

                _context.Participants.Update(participant);

                await _context.SaveChangesAsync();

                return _mapper.Map<ParticipantType>(participant);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}