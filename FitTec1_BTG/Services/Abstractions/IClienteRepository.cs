using FitTec1_BTG.Model;

namespace FitTec1_BTG.Services.Abstractions
{
    public interface IClienteRepository
    {
        Task<List<Cliente>> GetAllAsync();
        Task AddAsync(Cliente cliente);
        Task UpdateAsync(Cliente cliente);
        Task DeleteAsync(int id);
    }
}
