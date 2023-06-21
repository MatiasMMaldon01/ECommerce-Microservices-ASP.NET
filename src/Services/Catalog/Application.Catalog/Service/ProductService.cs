using MongoDB.Bson;
using Domain.Catalog.Interfaces;
using Domain.Catalog.Entities;
using Application.Catalog.Interfaces;
using Application.Catalog.DTO_s;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Application.Catalog.Service
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _repository;
        private readonly ICategoryService _categoryService;

        public ProductService(IRepository<Product> repository, ICategoryService categoryService)
        {
            _repository = repository;
            _categoryService = categoryService;
        }

        public void Create(BaseDTO entity)
        {
            var dto = (ProductDTO)entity;

            var newMember = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                ImageFile = dto.ImageFile,
                Price = dto.Price,
                Summary = dto.Summary,
                CategoryId = ObjectId.Parse(dto.CategoryId),
            };

            _repository.Create(newMember);
        }

        public void Update(BaseDTO entity)
        {
            var dto = (ProductDTO)entity;
            ObjectId _id = new ObjectId(dto.Id);

            var newMember = new Product
            {
                Id = _id,
                Name = dto.Name,
                Description = dto.Description,
                ImageFile = dto.ImageFile,
                Price = dto.Price,
                Summary = dto.Summary,
                CategoryId = ObjectId.Parse(dto.CategoryId),
            };

            _repository.Update(newMember);
        }

        public void Delete(string id)
        {
            _repository.Delete(id);
        }

        public BaseDTO GetById(string id)
        {
            var product = _repository.GetById(id);

            return new ProductDTO
            {
                Id = product.Id.ToString(),
                Name = product.Name,
                Description = product.Description,
                ImageFile = product.ImageFile,
                Price = product.Price,
                Summary = product.Summary,
                CreatedAt = product.CreatedAt,
                CategoryId = product.CategoryId.ToString(),
                Category = HandleCategory(product.CategoryId.ToString())
            };
        }

        public IEnumerable<BaseDTO> GetByFilter(string? stringToFind)
        {
            IEnumerable<Product> request;
            ObjectId categoryId;
            FilterDefinition<Product> filter;

            if (string.IsNullOrEmpty(stringToFind))
            {
                request = _repository.GetByFilter();
            }
            else
            {
                if (ObjectId.TryParse(stringToFind, out categoryId))
                {
                    filter = Builders<Product>.Filter.Eq(x => x.CategoryId, categoryId);
                    request = _repository.GetByFilter(filter);
                }
                else
                {
                    filter = Builders<Product>.Filter.Where(x => x.Name.ToLower().Contains(stringToFind.ToLower()));
                    request = _repository.GetByFilter(filter);
                }
            }

            return request.Select(p => new ProductDTO
            {               
                Id = p.Id.ToString(),
                Name = p.Name,
                Description = p.Description,
                ImageFile = p.ImageFile,
                Price = p.Price,
                Summary = p.Summary,
                CreatedAt = p.CreatedAt,
                CategoryId = p.CategoryId.ToString(),
                Category = HandleCategory(p.CategoryId.ToString())
            })
                .OrderBy(p => p.CreatedAt)
                .ToList();
        }

        // Private Methods

        private string HandleCategory(string categoryId)
        {
            CategoryDTO category = (CategoryDTO)_categoryService.GetById(categoryId);

            return category.Description;
        }

    }
}
