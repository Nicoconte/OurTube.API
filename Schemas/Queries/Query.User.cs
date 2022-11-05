using HotChocolate.Resolvers;
using MediatR;
using OurTube.API.Schemas.Types;
using OurTube.API.UseCases.Users.Queries;
using System.Security.Claims;

namespace OurTube.API.Schemas.Queries
{
    public partial class Query
    {
        public async Task<UserType> GetMe(IResolverContext context, [Service] IMediator mediator) => await mediator.Send(new GetUserByIdQuery() { Id = context.GetGlobalValue<String>("CurrentUserId") ?? String.Empty });

        public async Task<List<UserType>> GetUsers([Service] IMediator mediator)
        {
            return await mediator.Send(new ListUsersQuery());
        }
    }
}
