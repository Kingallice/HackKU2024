using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace HackKU2024NourishNet.Models
{
    [BsonIgnoreExtraElements]
    public class User
    {
        [JsonIgnore]
        [BsonElement("_id")]
        public ObjectId ObjectId { get; set; }

        [BsonIgnore]
        public string Id { get {  return ObjectId.ToString(); } }

        [JsonIgnore]
        [BsonElement("org_id")]
        public ObjectId Org { get; set; }

        [BsonIgnore]
        public string OrgId { get { return Org.ToString(); } }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("fname")]
        public string FirstName { get; set; }

        [BsonElement("lname")]
        public string LastName { get; set; }

        [BsonElement("phone")]
        public string Phone { get; set; }

        [JsonIgnore]
        [BsonElement("password")]
        public string Password { get; set; }
    }
}
