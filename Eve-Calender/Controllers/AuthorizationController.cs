using Eve_Calender.Models;
using Eve_Calender.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Eve_Calender.Controllers
{
    [Produces("application/json")]
    [Route("api/Authorization")]
    public class AuthorizationController : Controller
    {

        private AuthorizationService authorizationService;
        private MongoContext context;

        public AuthorizationController(AuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
            this.context = new MongoContext();            
        }

        [HttpGet]
        [Route("code")]
        public async Task<IActionResult> Index(string code = null)
        {
            if (code == null || code.Length == 0)
            {
                return new JsonResult(new {
                    Error = "No code is given, a code is needed to resolve the character"
                });
            }

            AccessTokenModel accessToken = await authorizationService.getAccessToken(code);
            accessToken.CharacterId = await authorizationService.verifyCharacterAsync(accessToken);

            var collection = context.Database.GetCollection<AccessTokenModel>("AccessToken");
            var filter = new BsonDocument("_id", accessToken.CharacterId);

            var oldAccessToken = (await collection.FindAsync(filter)).FirstOrDefault();
                //.AccessTokens.Find(accessToken.CharacterId);

            if (oldAccessToken == null)
            {
                accessToken.UniqueId = Guid.NewGuid();
                accessToken.ExpiryDate = DateTime.Now.AddSeconds(accessToken.ExpiresIn - 120);
                collection.InsertOne(accessToken);
            } else
            {
                var update = Builders<AccessTokenModel>.Update
                    .Set(x => x.AccessToken, accessToken.AccessToken)
                    .Set(x => x.UniqueId, oldAccessToken.UniqueId)
                    .Set(x => x.RefreshToken, accessToken.RefreshToken)
                    .Set(x => x.ExpiresIn, accessToken.ExpiresIn)
                    .Set(x => x.ExpiryDate, DateTime.Now.AddSeconds(accessToken.ExpiresIn - 120));
                
                collection.UpdateOne(filter, update);
                accessToken.UniqueId = oldAccessToken.UniqueId;
            }            
            
            return new RedirectToActionResult("Result", "Home", new ResultModel {
                UniqueId = accessToken.UniqueId,
                CharacterId = accessToken.CharacterId
            });            
        }
    }
}