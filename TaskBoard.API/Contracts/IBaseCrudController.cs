using Microsoft.AspNetCore.Mvc;
using TaskBoard.API.Dtos;

namespace TaskBoard.API.Contracts
{
    public interface IBaseCrudController<T> where T : BaseDto
    {
        Task<IActionResult> Get(int id);
        Task<IActionResult> GetAll();
        Task<IActionResult> Add(T dto);
        Task<IActionResult> Delete(int id);
        Task<IActionResult> Update(int id, T dto);
    }
}
