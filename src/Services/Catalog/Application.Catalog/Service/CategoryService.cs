using Application.Catalog.DTO_s;
using Application.Catalog.Interfaces;
using Domain.Catalog.Entities;
using Domain.Catalog.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Application.Catalog.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _repository;

        public CategoryService(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public void Create(BaseDTO entity)
        {
            var dto = (CategoryDTO)entity;

            var newMember = new Category
            {
                Description = dto.Description,
            };

            _repository.Create(newMember);
        }

        public void Update(BaseDTO entity)
        {
            var dto = (CategoryDTO)entity;
            ObjectId _id = new ObjectId(dto.Id);

            var newMember = new Category
            {
                Id = _id,
                Description = dto.Description,
            };

            _repository.Update(newMember);
        }

        public void Delete(string id)
        {
            _repository.Delete(id);
        }

        public BaseDTO GetById(string id)
        {
            var category = _repository.GetById(id);

            return new CategoryDTO
            {
                Id = category.Id.ToString(),
                Description = category.Description,
                CreatedAt = category.CreatedAt,
            };
        }

        public IEnumerable<BaseDTO> GetByFilter(string stringToFind)
        {
            IEnumerable<Category> request;

            if (string.IsNullOrEmpty(stringToFind))
            {
                request = _repository.GetByFilter();
            }
            else
            {
                FilterDefinition<Category> filter = Builders<Category>.Filter.Where(x => x.Description.Contains(stringToFind.ToLower()));
                request = _repository.GetByFilter(filter);
            }


            return request.Select(c => new CategoryDTO
            {
                Id = c.Id.ToString(),
                Description = c.Description,
                CreatedAt = c.CreatedAt,
            })
                .OrderBy(c => c.CreatedAt)
                .ToList();
        }
    }
}
