using harjoitustyo.Models;
using harjoitustyo.Repositories;
using harjoitustyo.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using NuGet.Protocol.Core.Types;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace harjoitustyo.Middlewave
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserRepository _repository;
        private readonly IUserAuthenticationService _userAuthenticationService;
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IUserRepository repository) : base(options, logger, encoder, clock)
        {
        }  

            protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
            {
            var userName= "";
            var password= "";
            User? user;
                var endpoint = Context.GetEndpoint();
                if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
                {
                    return AuthenticateResult.NoResult();
                }

                if (!Request.Headers.ContainsKey("Authorization"))
                {
                    return AuthenticateResult.Fail("Authorization header missing");
                }
                try
                {
                    var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                    var credentialData = Convert.FromBase64String(authHeader.Parameter);
                    var credential = Encoding.UTF8.GetString(credentialData).Split(new[] { ':' }, 2);
                    userName = credential[0];
                    password = credential[1];

                user = await _repository.GetUserAsync(userName);

                if(user == null)
                {
                    return AuthenticateResult.Fail("Unauthorized");
                }
                if (user.Password != password)
                {
                    return AuthenticateResult.Fail("Unauthorized");
                }

                }
                catch
                {
                    return AuthenticateResult.Fail("Unauthorized");
                }

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, userName)
                };



                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                return AuthenticateResult.Success(ticket);
           

            }
        
    }
}

