using OurTube.API.Schemas.Types;

namespace OurTube.API.Schemas.Results
{
    public class LoginUserResultType
    {
        public TokenType Token { get; set; }
        public String UserLogged { get; set; }
    }
}
