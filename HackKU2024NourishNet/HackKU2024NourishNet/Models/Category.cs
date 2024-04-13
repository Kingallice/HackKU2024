using MongoDB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace HackKU2024NourishNet.Models
{
    public class Category
    {
        [JsonIgnore]
        [BsonElement("_id")]
        public BsonObjectId Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
    }
}
