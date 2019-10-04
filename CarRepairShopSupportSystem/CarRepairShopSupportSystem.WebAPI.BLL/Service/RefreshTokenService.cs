using CarRepairShopSupportSystem.WebAPI.BLL.IService;
using CarRepairShopSupportSystem.WebAPI.DAL.Abstraction;
using CarRepairShopSupportSystem.WebAPI.DAL.Models;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.WebAPI.BLL.Service
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRepository<RefreshToken> refreshTokenRepository;
        private readonly ICryptographyService cryptographyService;

        public RefreshTokenService(IRepository<RefreshToken> refreshTokenRepository, ICryptographyService cryptographyService)
        {
            this.refreshTokenRepository = refreshTokenRepository;
            this.cryptographyService = cryptographyService;
        }

        public  async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            //Get the client ID from the Ticket properties
            var clientCode = context.Ticket.Properties.Dictionary["client_id"];

            if (string.IsNullOrEmpty(clientCode))
            {
                return;
            }

            //Generating a Uniqure Refresh Token ID
            var refreshTokenId = Guid.NewGuid().ToString("n");

            // Getting the Refesh Token Life Time From the Owin Context
            var refreshTokenLifeTime = context.OwinContext.Get<string>("ta:clientRefreshTokenLifeTime");

            //Creating the Refresh Token object
            var token = new RefreshToken()
            {
                //storing the RefreshTokenId in hash format
                RefreshTokenId = Convert.ToBase64String(cryptographyService.GenerateSHA512(refreshTokenId)),
                ClientCode = clientCode,
                UserName = context.Ticket.Identity.Name,
                IssuedTime = DateTime.UtcNow,
                ExpiredTime = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime))
            };

            //Setting the Issued and Expired time of the Refresh Token
            context.Ticket.Properties.IssuedUtc = token.IssuedTime;
            context.Ticket.Properties.ExpiresUtc = token.ExpiredTime;

            token.ProtectedTicket = context.SerializeTicket();

            var existingToken = refreshTokenRepository.FindFirstBy(r => r.UserName == token.UserName
                                                                    && r.ClientCode == token.ClientCode);

            if (existingToken != null)
            {
                refreshTokenRepository.Remove(existingToken.RefreshTokenId);
                refreshTokenRepository.SaveChanges();
            }

            refreshTokenRepository.Add(token);
            var result = await refreshTokenRepository.SaveChangesAsync();
            
            if (result)
            {
                context.SetToken(refreshTokenId);
            }
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("ta:clientAllowedOrigin");
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            string hashedTokenId = Convert.ToBase64String(cryptographyService.GenerateSHA512(context.Token));

            var refreshToken = await refreshTokenRepository.FindFirstByAsync(rt => rt.RefreshTokenId == hashedTokenId);

            if (refreshToken != null)
            {
                //Get protectedTicket from refreshToken class
                context.DeserializeTicket(refreshToken.ProtectedTicket);
                refreshTokenRepository.Remove(refreshToken.RefreshTokenId);
                refreshTokenRepository.SaveChanges();
            }
        }
    }
}
