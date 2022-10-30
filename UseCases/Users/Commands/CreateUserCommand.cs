using AutoMapper;
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
        public String Username { get; set; }
        public String Password { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserType>
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public CreateUserCommandHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserType> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = new User()
                {
                    Id = Guid.NewGuid().ToString(),
                    Username = request.Username,
                    Password = request.Password,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                _context.Users.Add(user);

                await _context.SaveChangesAsync();

                var userType = _mapper.Map<UserType>(user);

                return userType;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
