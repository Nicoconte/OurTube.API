using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OurTube.API.Data;
using OurTube.API.Entities;
using OurTube.API.Schemas.Types;

namespace OurTube.API.UseCases.Rooms.Queries
{
    public class ListRoomsQuery : IRequest<List<RoomType>>
    {
        public string? Privacy { get; set; } = "all";
        public String? OrderBy { get; set; } = "asc";
        public String? Name { get; set; } = String.Empty;
        public String? OwnerId { get; set; } = String.Empty;
    }

    public class ListRoomsQueryHandler : IRequestHandler<ListRoomsQuery, List<RoomType>>
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public ListRoomsQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<RoomType>> Handle(ListRoomsQuery request, CancellationToken cancellationToken)
        {
            var roomQuery = _context.Rooms.ProjectTo<RoomType>(_mapper.ConfigurationProvider).AsQueryable();

            if (!string.IsNullOrEmpty(request.OwnerId))
                roomQuery = roomQuery.Where(c => c.Owner.Id == request.OwnerId);

            if (request.Privacy != "all" && request.Privacy == "public")
                roomQuery = roomQuery.Where(c => c.Configuration.IsPublic == true);

            if (request.Privacy != "all" && request.Privacy == "private")
                roomQuery = roomQuery.Where(c => c.Configuration.IsPublic == false);

            if (request.Privacy == "all" || request.Privacy == "*")
                roomQuery = roomQuery.Select(c => c);

            if (!string.IsNullOrEmpty(request.Name))
                roomQuery = roomQuery.Where(c => c.Name.Contains(request.Name));

            if (request.OrderBy == "asc")
                roomQuery = roomQuery.OrderBy(c => c.CreatedAt);

            if (request.OrderBy == "desc")
                roomQuery = roomQuery.OrderByDescending(c => c.CreatedAt);

            return roomQuery.ToList();
        }
    }
}
