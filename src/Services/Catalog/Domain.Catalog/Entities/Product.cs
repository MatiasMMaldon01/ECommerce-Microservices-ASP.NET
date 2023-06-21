using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Catalog.Entities
{
    [BsonCollection("Product")]
    public class Product : Base
    {
        [BsonElement("Name")]
        public string Name { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }

        public string ImageFile { get; set; }

        public decimal Price { get; set; }

        [BsonElement("category_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId CategoryId { get; set; }
    }
}
