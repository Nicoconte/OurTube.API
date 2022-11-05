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
            if (String.IsNullOrEmpty(input.Username) || String.IsNullOrEmpty(input.Password))
            {
                throw new GraphQLException("Invalid arguments. Some fields are empty");
            }

            if (await mediator.Send(new GetUserByUsernameQuery() { Username = input.Username }) != null)
            {
                throw new GraphQLException("Username is already in use. Try another one");
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
            if (String.IsNullOrEmpty(input.Username) || String.IsNullOrEmpty(input.Password))
            {
                throw new GraphQLException("Invalid arguments. Some fields are empty");
            }

            var token = await mediator.Send(new LoginUserQuery()
            {
                Username = input.Username,
                Password = input.Password
            });

            if (token == null)
            {
                throw new GraphQLException("User doesnt exist");
            }

            return token;
        }
    }
}
