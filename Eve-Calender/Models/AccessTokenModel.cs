using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Eve_Calender.Models
{
    public class AccessTokenModel
    {
        [BsonId]
        [BsonElement("character_id")]
        public int CharacterId { get; set; }

        [BsonElement("unique_id")]
        public Guid UniqueId { get; set; }

        [BsonElement("access_token")]
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [BsonElement("token_type")]        
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [BsonElement("expires_in")]
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [BsonElement("expiry_date")]
        public DateTime ExpiryDate { get; set; }

        [BsonElement("refresh_token")]
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
