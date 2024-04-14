using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace HackKU2024NourishNet.Models
{
    public class Ticket
    {
        [JsonIgnore]
        [BsonId]
        public ObjectId ObjectId { get; set; }

        [JsonIgnore]
        [BsonElement("ticket_type")]
        public ObjectId TicketTypeId { get; set; }

        [BsonIgnore]
        public string TicketType { get { return TicketTypeId.ToString(); } }

        [JsonIgnore]
        [BsonElement("org_type")]
        public ObjectId OrgTypeId { get; set; }

        [BsonIgnore]
        public string OrgType { get { return OrgTypeId.ToString(); } }

        [JsonIgnore]
        [BsonElement("org_id")]
        public ObjectId OrgId { get; set; }

        [BsonIgnore]
        public string Organization { get { return OrgId.ToString(); } }

        [JsonIgnore]
        [BsonElement("category")]
        public ObjectId CategoryId { get; set; }

        [BsonIgnore]
        public string Category { get { return CategoryId.ToString(); } }

        [BsonElement("item_name")]
        public string ItemName { get; set; }

        [BsonElement("item_quantity")]
        public double ItemQuantity { get; set; }

        [BsonElement("item_label")]
        public string ItemLabel { get; set; }

        [BsonElement("claimed")]
        public bool IsClaimed {  get; set; }

        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; }

    }
}
