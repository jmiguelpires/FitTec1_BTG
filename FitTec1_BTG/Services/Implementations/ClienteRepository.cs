using FitTec1_BTG.Model;
using FitTec1_BTG.Services.Abstractions;
using SQLite;

namespace FitTec1_BTG.Services.Implementations
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly SQLiteAsyncConnection _db;

        public ClienteRepository(string dbPath)
        {
            _db = new SQLiteAsyncConnection(dbPath);
            _db.CreateTableAsync<Cliente>().Wait();
        }

        public async Task<List<Cliente>> GetAllAsync()
        {
            return await _db.Table<Cliente>().ToListAsync();
        }

        public async Task AddAsync(Cliente cliente)
        {
            await _db.InsertAsync(cliente);
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            await _db.UpdateAsync(cliente);
        }

        public async Task DeleteAsync(int id)
        {
            await _db.DeleteAsync<Cliente>(id);
        }
    }
}
