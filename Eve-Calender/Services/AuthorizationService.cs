using Eve_Calender.Models;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Eve_Calender.Services
{
    // Can't we get an OAuth2 library or something for this?
    public class AuthorizationService
    {
        private IHostingEnvironment env;

        public AuthorizationService(IHostingEnvironment env)
        {
            this.env = env;
        }
        public async Task<AccessTokenModel> getAccessToken(string code)
        {
            string requestUri = "https://login.eveonline.com/oauth/token";

            string authorization = getAuthorizationKey();

            // Build httpClient with default headers
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Basic {authorization}");
            client.DefaultRequestHeaders.Add("Host", "login.eveonline.com");            

            // Make http call
            HttpResponseMessage response = await client.PostAsync(requestUri, new StringContent($"grant_type=authorization_code&code={code}", Encoding.UTF8, "application/x-www-form-urlencoded"));
            string jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AccessTokenModel>(jsonString);
        }


        public async Task<AccessTokenModel> RefreshToken(AccessTokenModel accessToken)
        {
            string requestUri = "https://login.eveonline.com/oauth/token";
            string authorization = getAuthorizationKey();

            // Build httpClient with default headers
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Basic {authorization}");
            client.DefaultRequestHeaders.Add("Host", "login.eveonline.com");

            // Make http call
            HttpResponseMessage response = await client.PostAsync(requestUri, new StringContent($"grant_type=refresh_token&refresh_token={accessToken.RefreshToken}", Encoding.UTF8, "application/x-www-form-urlencoded"));
            string jsonString = await response.Content.ReadAsStringAsync();
            AccessTokenModel refreshed = JsonConvert.DeserializeObject<AccessTokenModel>(jsonString);
            refreshed.CharacterId = accessToken.CharacterId;
            refreshed.ExpiryDate = DateTime.Now.AddSeconds(refreshed.ExpiresIn - 120);
            refreshed.UniqueId = accessToken.UniqueId;

            return refreshed;
        }

        public async Task<int> verifyCharacterAsync(AccessTokenModel accessToken)
        {
            string verificationUri = "https://esi.evetech.net/verify/";

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken.AccessToken}");
            client.DefaultRequestHeaders.Add("Host", "esi.tech.ccp.is");
            client.DefaultRequestHeaders.Add("User-Agent", Program.USER_AGENT);

            string jsonString = await client.GetStringAsync(verificationUri);
            JObject json = JObject.Parse(jsonString);

            int characterId = json["CharacterID"].Value<int>();

            return characterId;
        }

        public string getAuthenticationUrl()
        {
            var url = "https://login.eveonline.com/oauth/authorize/";
            url += "?response_type=code";
            url += $"&redirect_uri={HttpUtility.UrlEncode(Program.REDIRECT_URL)}";
            url += $"&client_id={HttpUtility.UrlEncode(Program.CLIENT_ID)}";
            url += $"&scope={HttpUtility.UrlEncode("esi-calendar.read_calendar_events.v1")}";

            return url;
        }


        private string getAuthorizationKey()
        {
            // Build authorization string
            string clientId =  Program.CLIENT_ID;            
            string clientSecret = Program.CLIENT_SECRET;
            return Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
        }
    }
}
