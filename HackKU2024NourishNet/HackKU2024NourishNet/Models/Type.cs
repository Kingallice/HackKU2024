using MongoDB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace HackKU2024NourishNet.Models
{
    public class Type
    {
        [JsonIgnore]
        [BsonElement("_id")]
        public BsonObjectId ObjectID { get; set; }
        [BsonIgnore]
        public string Id
        {
            get
            {
                return ObjectID.ToString();
            }
        }

        [BsonElement("name")]
        public string Name { get; set; }
    }
}
