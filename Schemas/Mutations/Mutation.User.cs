using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Resolvers;
using MediatR;
using OurTube.API.Schemas.Inputs;
using OurTube.API.Schemas.Types;
using OurTube.API.UseCases.Users.Commands;
using OurTube.API.UseCases.Users.Queries;
using System.Security.Claims;

namespace OurTube.API.Schemas.Mutations
{
    public partial class Mutation
    {
        public async Task<UserType> SignUp([Service] IMediator mediator, IResolverContext context, CreateUserInputType input)
        {
            if (await mediator.Send(new CheckUsernameInUseQuery() { Username = input.Username }))
            {
                context.ReportError(new Error("Username is already in use. Try another one", "ERROR_USERNAME_ALREADY_EXISTS"));
                return null;
            }

            var type = await mediator.Send(new CreateUserCommand()
            {
                Password = input.Password,
                Username = input.Username
            });

            return type;
        }

        public async Task<TokenType> SignIn([Service] IMediator mediator, IResolverContext context, LoginUserInputType input)
        {
            var token = await mediator.Send(new LoginUserQuery()
            {
                Username = input.Username,
                Password = input.Password
            });

            if (token == null)
            {
                context.ReportError(new Error("User doesnt exist", "ERROR_USER_NOT_FOUND"));
                return null;
            }

            return token;
        }
    }
}
