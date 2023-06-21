using MongoDB.Bson;

namespace Application.Catalog.DTO_s
{
    public class ProductDTO : BaseDTO
    {
        public string Name { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }

        public string ImageFile { get; set; }

        public decimal Price { get; set; }

        public string CategoryId { get; set; }

        public string Category { get; set; }
    }
}
