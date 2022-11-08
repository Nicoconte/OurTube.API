using HotChocolate.Resolvers;
using MediatR;
using OurTube.API.Entities;
using OurTube.API.Helpers;
using OurTube.API.Schemas.Inputs;
using OurTube.API.Schemas.Types;
using OurTube.API.UseCases.Rooms.Queries;
using OurTube.API.UseCases.Users.Commands;

namespace OurTube.API.Schemas.Queries
{
    public partial class Query
    {

        public async Task<List<ParticipantType>> GetBannedUsersFromRoom([Service] IMediator mediator, string roomId)
        {
            return await mediator.Send(new GetBannedUsersFromRoomQuery() { RoomId = roomId });   
        }


        public async Task<List<RoomType>> GetAllRooms([Service] IMediator mediator, AllRoomsInputType input)
        {
            return await mediator.Send(new ListRoomsQuery()
            {
                Privacy = input.PrivacyType,
                Name = input.RoomName,
                OrderBy = input.OrderBy
            });
        }

        public async Task<List<RoomType>> GetAllUserRooms([Service] IMediator mediator, IResolverContext context, AllRoomsInputType input)
        {
            var userId = context.GetGlobalValue<String>("CurrentUserId");

            return await mediator.Send(new ListRoomsQuery()
            {
                Privacy = input.PrivacyType,
                Name = input.RoomName,
                OrderBy = input.OrderBy,
                OwnerId = userId
            });
        }


        public async Task<RoomType> RoomById([Service] IMediator mediator, RoomByIdInputType input)
        {
            var result = await _validators.RoomByIdInputTypeValidator.ValidateAsync(input);

            if (!result.IsValid)
            {
                FluentValidationHelper.RaiseGraphQLException(result.Errors);
            }

            return await mediator.Send(new GetRoomByIdQuery() { RoomId = input.RoomId });
        }
    }
}
