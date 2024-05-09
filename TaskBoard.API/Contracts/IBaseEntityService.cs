using TaskBoard.API.Dtos;

namespace TaskBoard.API.Contracts
{
    public interface IBaseEntityService<T> where T : BaseDto
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int id);
        Task Add(T model);
        Task Update(int id, T model);
        Task Delete(int id);
    }
}
