using Application.Catalog.DTO_s;

namespace Application.Catalog.Interfaces
{
    public interface IServiceBase
    {
        void Create(BaseDTO entity);
        void Update(BaseDTO entity);
        void Delete(string id);


        BaseDTO GetById(string id);
        IEnumerable<BaseDTO> GetByFilter(string? stringToFind);
    }
}
