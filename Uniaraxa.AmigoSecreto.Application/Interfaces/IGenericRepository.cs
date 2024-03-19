using System.Collections.Generic;
using System.Threading.Tasks;

namespace Uniaraxa.AmigoSecreto.Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> ObterPorIdAsync(long id);
        Task<IReadOnlyList<T>> ObterTodosAsync();
        Task<long> InserirAsync(T entity);
        Task<long> AtualizarAsync(T entity);
        Task<long> RemoverAsync(long id);
    }
}
