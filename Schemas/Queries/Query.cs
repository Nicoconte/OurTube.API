using MediatR;
using OurTube.API.Schemas.Inputs;
using OurTube.API.Schemas.Results;
using OurTube.API.Schemas.Types;
using OurTube.API.UseCases.Users.Commands;

namespace OurTube.API.Schemas.Queries
{
    public class Query
    {
        public UserType GetUser() => new UserType() { };
    }
}
