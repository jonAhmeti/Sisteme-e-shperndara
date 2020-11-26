using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApiTest.Models;

namespace WebApiTest.Security
{
    public class MyAuthenticationService : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly WebAPIContext _context;
        private readonly IConfiguration _configuration;
        private ILogger<MyAuthenticationService> _logger;

        public MyAuthenticationService(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory loggerFactory,
            UrlEncoder encoder, ISystemClock clock, WebAPIContext context, IConfiguration configuration, ILogger<MyAuthenticationService> logger)
            : base(options, loggerFactory, encoder, clock)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (Request.Headers.ContainsKey("Authorization"))
            {
                byte[] headerValueBytes = Convert.FromBase64String(Request.Headers["Authorization"]);
                string emailPassword = Encoding.UTF8.GetString(headerValueBytes);
                string[] parts = emailPassword.Split(':');
                if (parts.Length != 2)
                {
                    return Task.FromResult(AuthenticateResult.Fail("Both username and password is required."));
                }

                string username = parts[0];
                string password = parts[1];
                var user = _context.Users.SingleOrDefault(u => u.Username == username && u.Password == password);
                if (user == null)
                {
                    return Task.FromResult(AuthenticateResult.Fail("Couldn't find user with provided credentials."));
                }
                var tokenService = new TokenService(_configuration);

                var claims = new[]
                {
                    new Claim("username", username),
                    new Claim("id", user.Id.ToString()),
                    new Claim("role", user.RoleId.ToString())
                };
                //? ClaimsIdentity creation with claims.
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                _logger.LogInformation($"{Context.Request.Path.Value} was called from MyAuthenticationService successfully");
                return Task.FromResult(AuthenticateResult.Success(ticket));
            }

            return Task.FromResult(AuthenticateResult.Fail("Authorization header not found."));
        }

        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            await base.HandleChallengeAsync(properties);
        }
    }
}
