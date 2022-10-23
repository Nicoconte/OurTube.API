using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace OurTube.API.Helpers
{
    public class PasswordHelper
    {
        public static string Hash(string password)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                password: password,
                                salt: new byte[128 / 8],
                                prf: KeyDerivationPrf.HMACSHA256,
                                iterationCount: 10000,
                                numBytesRequested: 256 / 8
                            ));
        }
    }
}
