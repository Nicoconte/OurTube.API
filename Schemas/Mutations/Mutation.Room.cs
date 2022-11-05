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

            if (String.IsNullOrEmpty(input.Name))
            {
                throw new GraphQLException("Room name cannot be empty");
            }

            if (!input.Configuration.IsPublic && string.IsNullOrEmpty(input.Configuration.Password))
            {
                throw new GraphQLException("Password cannot be empty");
            }

            if (input.Configuration.MaxParticipants > 15)
            {
                throw new GraphQLException("The maximum amount of participant is 15.");
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
            if (String.IsNullOrEmpty(input.ParticipantUsername) || String.IsNullOrEmpty(input.RoomId))
            {
                throw new GraphQLException("Invalid arguments. Some fields are empty");
            }

            if (await mediator.Send(new GetUserByUsernameQuery() { Username = input.ParticipantUsername}) == null)
            {
                throw new GraphQLException($"User with username '{input.ParticipantUsername}' does not exist");
            }

            if (await mediator.Send(new GetRoomByIdQuery() { RoomId = input.RoomId }) == null)
            {
                throw new GraphQLException($"Room with ID '{input.RoomId}' does not exist");
            }

            var roomParticipant = await mediator.Send(new GetParticipantFromRoomQuery()
            {
                Username = input.ParticipantUsername,
                RoomId = input.RoomId
            });

            if (roomParticipant == null)
            {
                throw new GraphQLException($"The user requested is not a participant of this room");
            }

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
            if (String.IsNullOrEmpty(input.ParticipantUsername) || String.IsNullOrEmpty(input.RoomId))
            {
                throw new GraphQLException("Invalid arguments. Some fields are empty");
            }

            if (await mediator.Send(new GetUserByUsernameQuery() { Username = input.ParticipantUsername }) == null)
            {
                throw new GraphQLException($"User with username '{input.ParticipantUsername}' does not exist");
            }

            var room = await mediator.Send(new GetRoomByIdQuery() { RoomId = input.RoomId });

            if (room == null)
            {
                throw new GraphQLException($"Room with ID '{input.RoomId}' does not exist");
            }

            var userId = context.GetGlobalValue<String>("CurrentUserId");

            if (room.Owner.Id != userId)
            {
                throw new GraphQLException("Only the owner of this room can perform this action");
            }

            if (userId == input.ParticipantUsername)
            {
                throw new GraphQLException("You're the owner of this room. You cannot add yourself!");
            }

            var roomParticipant = await mediator.Send(new GetParticipantFromRoomQuery()
            {
                Username = input.ParticipantUsername, RoomId = input.RoomId
            });

            if (roomParticipant != null || (roomParticipant != null && roomParticipant.IsBanned))
            {
                throw new GraphQLException("User cannot be added. He might be banned from this room o he is already in");
            }

            if (!room.Configuration.IsPublic && (PasswordHelper.Hash(input.RoomPassword) != room.Configuration.Password || string.IsNullOrEmpty(input.RoomPassword)))
            {
                throw new GraphQLException("Password is incorrect or empty.");
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
