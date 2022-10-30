using HotChocolate.Resolvers;
using MediatR;
using Microsoft.AspNetCore.Connections.Features;
using OurTube.API.Entities;
using OurTube.API.Helpers;
using OurTube.API.Schemas.Inputs;
using OurTube.API.Schemas.Types;
using OurTube.API.UseCases.Rooms.Commands;
using OurTube.API.UseCases.Rooms.Queries;
using OurTube.API.UseCases.Users.Queries;

namespace OurTube.API.Schemas.Mutations
{
    public partial class Mutation
    {
        public async Task<RoomType> CreateRoom([Service] IMediator mediator, IResolverContext context, CreateRoomInputType input)
        {
            String userId = context.GetGlobalValue<String>("CurrentUserId");

            if (!input.Configuration.IsPublic && string.IsNullOrEmpty(input.Configuration.Password))
            {
                context.ReportError(new Error("Password cannot be empty.", "ERROR_USER_BANNED"));
                return null;
            }

            if (input.Configuration.MaxParticipants > 15)
            {
                context.ReportError(new Error("15 is max amount of participants for a room.", "ERROR_USER_BANNED"));
                return null;
            }

            var roomCreated = await mediator.Send(new CreateRoomCommand()
            {
                Room = new RoomType()
                {
                    Name = input.Name
                },
                RoomConfiguration = new RoomConfigurationType()
                {
                    IsPublic = input.Configuration.IsPublic,
                    MaxParticipants = input.Configuration.MaxParticipants,
                    Password = input.Configuration.Password
                },
                OwnerId = userId
            });

            return roomCreated;
        }
        
        public async Task<ParticipantType> BanOrUnbanParticipantFromRoom([Service] IMediator mediator, BanOrUnbanParticipantInputType input)
        {
            var participantModified = await mediator.Send(new BanOrUnbanParticipantCommand()
            {
                ParticipantUsername = input.ParticipantUsername,
                RoomId = input.RoomId,
                Operation = input.Operation
            });

            return participantModified;
        }

        public async Task<ParticipantType> AddParticipantToRoom([Service] IMediator mediator, IResolverContext context, AddParticipantToRoomInputType input)
        {
            var room = await mediator.Send(new GetRoomByIdQuery() { RoomId = input.RoomId });

            var userId = context.GetGlobalValue<String>("CurrentUserId");

            if (room.Owner.Id != userId)
            {
                context.ReportError(new Error("Only the owner of this room can perform this action", "ERROR_INVALID_ROLE"));
                return null;
            }

            if (userId == input.ParticipantUsername)
            {
                context.ReportError(new Error("You're the owner of this room. You cannot add yourself!", "ERROR_INVALID_OPERATION"));
                return null;
            }

            var roomParticipant = await mediator.Send(new GetParticipantFromRoomQuery()
            {
                Username = input.ParticipantUsername, RoomId = input.RoomId
            });

            if (roomParticipant != null || (roomParticipant != null && roomParticipant.IsBanned))
            {
                context.ReportError(new Error("User cannot be added. He might be banned from this room o he is already in", "ERROR_INVALID_OPERATION"));
                return null;
            }

            if (!room.Configuration.IsPublic && (PasswordHelper.Hash(input.RoomPassword) != room.Configuration.Password || string.IsNullOrEmpty(input.RoomPassword)))
            {
                context.ReportError(new Error("Password is incorrect or empty.", "ERROR_USER_BANNED"));
                return null;
            }

            var participant = await mediator.Send(new AddParticipantCommand()
            {
                ParticipantUsername = input.ParticipantUsername,
                RoomId = input.RoomId
            });

            return participant;
        }
    }
}
