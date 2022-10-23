using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Resolvers;
using MediatR;
using OurTube.API.Schemas.Inputs;
using OurTube.API.Schemas.Results;
using OurTube.API.Schemas.Types;
using OurTube.API.UseCases.Users.Commands;
using OurTube.API.UseCases.Users.Queries;
using System.Security.Claims;

namespace OurTube.API.Schemas.Mutations
{
    public partial class Mutation
    {
        public async Task<CreateUserResultType> RegisterUser([Service] IMediator mediator, IResolverContext context, CreateUserInputType input)
        {
            if (await mediator.Send(new CheckUsernameInUseQuery() { Username = input.Username }))
            {
                context.ReportError(new Error("Username is already in use. Try another one", "ERROR_USERNAME_ALREADY_EXISTS"));
                return null;
            }

            var type = await mediator.Send(new CreateUserCommand()
            {
                UserType = new UserType()
                {
                    Username = input.Username,
                    Password = input.Password,
                }
            });

            return new CreateUserResultType()
            {
                Id = type.Id,
                Username = type.Username
            };
        }

        public async Task<LoginUserResultType> Login([Service] IMediator mediator, IResolverContext context, LoginUserInputType input)
        {
            var (id, token) = await mediator.Send(new LoginUserQuery()
            {
                UserType = new UserType()
                {
                    Username = input.Username,
                    Password = input.Password
                }
            });

            if (string.IsNullOrWhiteSpace(token))
            {
                context.ReportError(new Error("User doesnt exist", "ERROR_USER_NOT_FOUND"));
                return null;
            }

            return new LoginUserResultType()
            {
                UserLogged = id,
                Token = new TokenType()
                {
                    Value = token
                }
            };
        }
    }
}
