using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Catalog.Entities
{
    [BsonCollection("Category")]
    public class Category : Base
    {
        [BsonElement("Description")]
        public string Description { get; set; }
    }
}
