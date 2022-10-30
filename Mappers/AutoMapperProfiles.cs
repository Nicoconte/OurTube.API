using AutoMapper;
using OurTube.API.Entities;
using OurTube.API.Schemas.Types;

namespace OurTube.API.Mappers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserType>();
            CreateMap<Room, RoomType>();
            CreateMap<User, UserType>();
            CreateMap<Queue, QueueType>();
            CreateMap<Video, VideoType>();
            CreateMap<Participant, ParticipantType>();
            CreateMap<RoomConfiguration, RoomConfigurationType>();
        }
    }
}
