using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace HackKU2024NourishNet.Models
{
    public class Organization
    {
        [JsonIgnore]
        [BsonElement("_id")]
        public ObjectId ObjectId { get; set; }

        [BsonIgnore]
        public string Id 
        { 
            get
            {
                return ObjectId.ToString();
            }
        }

        [BsonElement("name")] 
        public string Name { get; set; }

        [JsonIgnore]
        [BsonElement("main_contact")] 
        public ObjectId MainContact { get; set; }

        [BsonIgnore]
        public string ContactId 
        { 
            get 
            {
                return MainContact.ToString();
            } 
        }

        [JsonIgnore]
        [BsonElement("org_type")]
        public ObjectId OrgType { get; set; }

        [BsonIgnore]
        public string TypeId { get {  return OrgType.ToString(); } }

        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; }

        [BsonIgnore]
        public List<User> AuthorizedUsers { get; set; } = new List<User>();

        //[BsonIgnore]
        //public List<Locations> Locations { get; set; }
    }
}
