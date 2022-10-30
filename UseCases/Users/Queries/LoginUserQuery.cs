using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OurTube.API.Data;
using OurTube.API.Helpers;
using OurTube.API.Schemas.Inputs;
using OurTube.API.Schemas.Types;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OurTube.API.UseCases.Users.Queries
{
    public class LoginUserQuery : IRequest<TokenType>
    {
        public String Username { get; set; }
        public String Password { get; set; }
    }

    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, TokenType>
    {
        private ApplicationDbContext _context;
        private IConfiguration _configuration; 

        public LoginUserQueryHandler(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<TokenType> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context
                    .Users
                    .FirstOrDefaultAsync(c =>
                        c.Username == request.Username &&
                        c.Password == PasswordHelper.Hash(request.Password)
                    ); ;

                if (user is null) return null;

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

                return new TokenType()
                {
                    UserLogged = user.Username,
                    Token = token
                };
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
