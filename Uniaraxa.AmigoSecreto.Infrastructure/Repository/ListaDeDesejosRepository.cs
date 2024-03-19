using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;
using Uniaraxa.AmigoSecreto.Application.Interfaces;
using Uniaraxa.AmigoSecreto.Core.Entities;

namespace Uniaraxa.AmigoSecreto.Infrastructure.Repository
{
    public class ListaDeDesejosRepository : IListaDeDesejosRepository
    {
        private readonly string _connectionString;

        public ListaDeDesejosRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IReadOnlyList<ListaDeDesejos>> ObterTodosAsync()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                return (IReadOnlyList<ListaDeDesejos>)await connection.QueryAsync<ListaDeDesejos>("SELECT * FROM listadesejos");
            }
        }

        public async Task<ListaDeDesejos> ObterPorIdAsync(long id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.QueryFirstOrDefaultAsync<ListaDeDesejos>("SELECT * FROM listadesejos WHERE id = @Id", new { Id = id });
            }
        }

        public async Task<long> InserirAsync(ListaDeDesejos entity)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.ExecuteAsync("INSERT INTO listadesejos (nome, descricao, id_participante) VALUES (@Nome, @Descricao, @ParticipanteId)", entity);
            }
        }

        public async Task<long> AtualizarAsync(ListaDeDesejos entity)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.ExecuteAsync("UPDATE listadesejos SET nome = @Nome, descricao = @Descricao, id_participante = @ParticipanteId WHERE id = @Id", entity);
            }
        }

        public async Task<long> RemoverAsync(long id) // Alteração feita aqui
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.ExecuteAsync("DELETE FROM listadesejos WHERE id = @Id", new { Id = id });
            }
        }
    }
}
