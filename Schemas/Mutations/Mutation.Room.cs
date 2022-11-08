using FluentValidation.AspNetCore;
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

            var result = await _validators.CreateRoomInputValidator.ValidateAsync(input);

            if (!result.IsValid)
            {
                FluentValidationHelper.RaiseGraphQLException(result.Errors);
            }

            var configResult = await _validators.CreateRoomConfigInputValidator.ValidateAsync(input.Configuration);

            if (!configResult.IsValid)
            {
                FluentValidationHelper.RaiseGraphQLException(configResult.Errors);
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
            var result = await _validators.BanOrUnbanParticipantInputValidator.ValidateAsync(input);

            if (!result.IsValid)
            {
                FluentValidationHelper.RaiseGraphQLException(result.Errors);
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

            var result = await _validators.AddParticipantToRoomInputValidator.ValidateAsync(input);

            if (!result.IsValid)
            {
                FluentValidationHelper.RaiseGraphQLException(result.Errors);
            }

            var participant = await mediator.Send(new AddParticipantCommand()
            {
                ParticipantUsername = input.ParticipantUsername,
                RoomId = input.RoomId,
                RoomPassword = input?.RoomPassword ?? String.Empty,
                RoomOwnerID = context.GetGlobalValue<String>("CurrentUserId") ?? String.Empty
        });

            return participant;
        }
    }
}
