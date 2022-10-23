using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OurTube.API.Data;
using OurTube.API.Helpers;
using OurTube.API.Schemas.Inputs;
using OurTube.API.Schemas.Results;
using OurTube.API.Schemas.Types;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OurTube.API.UseCases.Users.Queries
{
    public class LoginUserQuery : IRequest<(string Id, string Token)>
    {
        public UserType UserType { get; set; }
    }

    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, (string Id, string Token)>
    {
        private ApplicationDbContext _context;
        private IConfiguration _configuration; 

        public LoginUserQueryHandler(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<(string Id, string Token)> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context
                    .Users
                    .FirstOrDefaultAsync(c =>
                        c.Username == request.UserType.Username &&
                        c.Password == PasswordHelper.Hash(request.UserType.Password)
                    ); ;

                if (user is null) return (String.Empty, String.Empty);

                var secretKey = _configuration.GetSection("Jwt:key").Value;
                var keyAsBytes = Encoding.ASCII.GetBytes(secretKey);

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.Username)
                };

                var descriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyAsBytes), SecurityAlgorithms.HmacSha256)
                };

                var handler = new JwtSecurityTokenHandler();
                var newTokenCreated = handler.CreateToken(descriptor);

                var token = handler.WriteToken(newTokenCreated);

                return (user.Id, token);
            }
            catch(Exception ex)
            {
                return (string.Empty, string.Empty);
            }
        }
    }
}
