using CarRepairShopSupportSystem.WebAPI.BLL.IService;
using CarRepairShopSupportSystem.WebAPI.DAL.Abstraction;
using CarRepairShopSupportSystem.WebAPI.DAL.Models;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.WebAPI.BLL.Provider
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly IRepository<Client> clientRepository;
        private readonly ILoginService loginService;

        public AuthorizationServerProvider(IRepository<Client> clientRepository, ILoginService loginService)
        {
            this.clientRepository = clientRepository;
            this.loginService = loginService;
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientIdCode = string.Empty;
            string clientSecret = string.Empty;

            // The TryGetBasicCredentials method checks the Authorization header and
            // Return the ClientId and clientSecret
            if (!context.TryGetBasicCredentials(out clientIdCode, out clientSecret))
            {
                context.SetError("invalid_client", "Client credentials could not be retrieved through the Authorization header.");
                return Task.FromResult<object>(null);
            }

            //Check the existence of by calling the ValidateClient method
            Client client = clientRepository.FindBy(c => c.ClientIdCode == clientIdCode
                                                     && c.ClientSecret == clientSecret).FirstOrDefault();

            if (client == null)
            {
                // Client could not be validated.
                context.SetError("invalid_client", "Client credentials are invalid.");
                return Task.FromResult<object>(null);
            }
            else
            {
                if (!client.Active)
                {
                    context.SetError("invalid_client", "Client is inactive.");
                    return Task.FromResult<object>(null);
                }

                // Client has been verified.
                context.OwinContext.Set<Client>("ta:client", client);
                context.OwinContext.Set<string>("ta:clientAllowedOrigin", client.AllowedOrigin);
                context.OwinContext.Set<string>("ta:clientRefreshTokenLifeTime", client.RefreshTokenLifeTime.ToString());
                context.Validated();
                return Task.FromResult<object>(null);
            }
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            Client client = context.OwinContext.Get<Client>("ta:client");
            var allowedOrigin = context.OwinContext.Get<string>("ta:clientAllowedOrigin");

            if (allowedOrigin == null)
            {
                allowedOrigin = "*";
            }

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            User user = loginService.Login(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "Provided username and password is incorrect");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Role, user.Permission.Name));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Username));

            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                        "client_id", context.ClientId ?? string.Empty
                    },
                    {
                        "userName", context.UserName
                    }
                });

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["client_id"];
            var currentClient = context.ClientId;

            if (originalClient != currentClient)
            {
                context.SetError("invalid_clientId", "Refresh token is issued to a different clientId.");
                return Task.FromResult<object>(null);
            }

            // Change auth ticket for refresh token requests
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
            newIdentity.AddClaim(new Claim("newClaim", "newValue"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }
    }
}
