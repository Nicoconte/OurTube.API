using MediatR;
using OurTube.API.Data;
using OurTube.API.Entities;
using OurTube.API.Helpers;
using OurTube.API.Schemas.Inputs;
using OurTube.API.Schemas.Types;
using System.Runtime.InteropServices;

namespace OurTube.API.UseCases.Users.Commands
{
    public class CreateUserCommand : IRequest<UserType>
    {
        public UserType UserType { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserType>
    {
        private ApplicationDbContext _context;

        public CreateUserCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserType> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                request.UserType.Id = Guid.NewGuid().ToString();
                request.UserType.UpdatedAt = DateTime.Now;
                request.UserType.CreatedAt = DateTime.Now;
                request.UserType.Password = PasswordHelper.Hash(request.UserType.Password);

                var user = new User()
                {
                    Id = request.UserType.Id,
                    Username = request.UserType.Username,
                    Password = request.UserType.Password,
                    CreatedAt = request.UserType.CreatedAt,
                    UpdatedAt = request.UserType.UpdatedAt
                };

                _context.Users.Add(user);

                await _context.SaveChangesAsync();

                return request.UserType;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
