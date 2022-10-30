using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OurTube.API.Data;
using OurTube.API.Entities;
using OurTube.API.Helpers;
using OurTube.API.Schemas.Types;

namespace OurTube.API.UseCases.Rooms.Commands
{
    public class CreateRoomCommand : IRequest<RoomType>
    {
        public RoomType Room { get; set; }
        public String OwnerId { get; set; }
        public RoomConfigurationType RoomConfiguration { get; set; }
    }

    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, RoomType>
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public CreateRoomCommandHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RoomType> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var room = new Room()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = request.Room.Name,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    InviteCode = Guid.NewGuid().ToString().Substring(0, 6),
                    OwnerId = request.OwnerId,
                    Configuration = new RoomConfiguration()
                    {
                        Id = Guid.NewGuid().ToString(),
                        IsPublic = request.RoomConfiguration.IsPublic,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        MaxParticipants = request.RoomConfiguration.MaxParticipants == 0 ? 15 : request.RoomConfiguration.MaxParticipants,
                        Password = request.RoomConfiguration.IsPublic ? string.Empty : PasswordHelper.Hash(request.RoomConfiguration.Password)
                    },
                    Participants = new List<Participant>(),
                    Queue = new Queue()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Offset = 0,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        Videos = new List<Video>()
                    }
                };

                _context.Rooms.Add(room);

                await _context.SaveChangesAsync();

                return _mapper.Map<RoomType>(room);
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
