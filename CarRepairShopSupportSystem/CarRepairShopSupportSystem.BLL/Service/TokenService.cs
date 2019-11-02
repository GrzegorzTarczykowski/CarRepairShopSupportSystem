using CarRepairShopSupportSystem.BLL.IService;
using CarRepairShopSupportSystem.BLL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.BLL.Service
{
    public class TokenService : ITokenService
    {
        public Token GetToken(string clientId, string clientSecret, string username, string password)
        {
            Token token = new Token();
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            // Need to set the Client ID and Client Secret in the Authorization Header
            // in Base64 Encoded Format using the Basic Authentication as shown below
            string ClientIDandSecret = clientId + ":" + clientSecret;
            var authorizationHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes(ClientIDandSecret));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authorizationHeader);
            // Create a dictionary which contains the request form data, here we need to set
            // the username, password and grant_type as shown below
            var RequestBody = new Dictionary<string, string>
            {
                {"grant_type", "password"},
                {"username", username},
                {"password", password},
            };
            //Make a Post request by invoking the PostAsync method on the client object as shown below
            var tokenResponse = client.PostAsync(Setting.addressAPI + "token", new FormUrlEncodedContent(RequestBody)).Result;
            if (tokenResponse.IsSuccessStatusCode)
            {
                var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;
                token = JsonConvert.DeserializeObject<Token>(JsonContent);
                token.Error = null;
            }
            else
            {
                token.Error = "GetAccessToken failed likely due to an invalid client ID, client secret, or invalid usrename and password";
            }
            return token;
        }

        public Token GetTokenByRefreshToken(string clientId, string clientSecret, string refreshToken)
        {
            Token token = new Token();
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            // Need to set the Client ID and Client Secret in the Authorization Header
            // in Base64 Encoded Format using Basic Authentication as shown below
            string ClientIDandSecret = clientId + ":" + clientSecret;
            var authorizationHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes(ClientIDandSecret));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authorizationHeader);
            // Create a dictionary which contains the refresh token, here we need to set
            // the grant_type as refresh_token as shown below
            var RequestBody = new Dictionary<string, string>
            {
                {"grant_type", "refresh_token"},
                {"refresh_token", refreshToken}
            };
            //Make a Post request by invoking the PostAsync method on the client object as shown below
            var tokenResponse = client.PostAsync(Setting.addressAPI + "token", new FormUrlEncodedContent(RequestBody)).Result;
            if (tokenResponse.IsSuccessStatusCode)
            {
                var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;
                token = JsonConvert.DeserializeObject<Token>(JsonContent);
                token.Error = null;
            }
            else
            {
                token.Error = "GetAccessToken by Refresh Token failed likely due to an invalid client ID, client secret, or it has been revoked by the system admin";
            }
            return token;
        }
    }
}
