using HotChocolate.AspNetCore;
using HotChocolate.Execution;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OurTube.API.Schemas.Interceptors
{
    public class AuthorizationRequestInterceptor : DefaultHttpRequestInterceptor 
    {
        public override ValueTask OnCreateAsync(HttpContext context, IRequestExecutor requestExecutor, IQueryRequestBuilder requestBuilder, CancellationToken cancellationToken)
        {
            if (context.Request.Headers.ContainsKey("Authorization"))
            {
                var stream = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", String.Empty);

                var id = (new JwtSecurityTokenHandler().ReadToken(stream) as JwtSecurityToken).Claims.FirstOrDefault()?.Value;
                    
                requestBuilder.SetProperty("CurrentUserId", id);
            }

            return base.OnCreateAsync(context, requestExecutor, requestBuilder, cancellationToken);
        }
    }
}
