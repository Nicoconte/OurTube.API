using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OurTube.API.Data;
using OurTube.API.Entities;
using OurTube.API.Schemas.Types;

namespace OurTube.API.UseCases.Rooms.Commands
{
    public class AddParticipantCommand : IRequest<ParticipantType>
    {
        public String RoomId { get; set; }
        public String ParticipantUsername { get; set; }
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
            try
            {
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

                return _mapper.Map<ParticipantType>(newParticipant);
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
