using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Catalog.Entities
{
    [Serializable, BsonIgnoreExtraElements]
    public class Base
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("createdat")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedAt => Id.CreationTime;
    }
}
