using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Uniaraxa.AmigoSecreto.Application.Interfaces;
using Uniaraxa.AmigoSecreto.Core.Entities;
using Uniaraxa.AmigoSecreto.Core.Validador;

namespace Uniaraxa.AmigoSecreto.Infrastructure.Repository
{
    public class ParticipanteRepository : IParticipanteRepository
    {
        private readonly string _connectionString;

        public ParticipanteRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IReadOnlyList<Participante>> ObterTodosAsync()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<Participante>("SELECT * FROM participante");
                return result.AsList().AsReadOnly();
            }
        }
        public async Task<Participante> ObterPorIdAsync(long id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.QueryFirstOrDefaultAsync<Participante>("SELECT * FROM participante WHERE id = @Id", new { Id = id });
            }
        }

        public async Task<long> InserirAsync(Participante entity)
        {
            // Verificar se o e-mail é válido
            var emailValido = new ValidadorEmail(entity.Email).EmailEValido();
            if (!emailValido)
            {
                throw new ArgumentException("O e-mail especificado não está em um formato válido.");
            }

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.ExecuteAsync("INSERT INTO participante (nome, email, senha, id_login_service) VALUES (@Nome, @Email, @Senha, @LoginServiceId)", entity);
            }
        }

        public async Task<long> AtualizarAsync(Participante entity)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.ExecuteAsync("UPDATE participante SET nome = @Nome, email = @Email, senha = @Senha, id_login_service = @LoginServiceId WHERE id = @Id", entity);
            }
        }

        public async Task<long> RemoverAsync(long id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.ExecuteAsync("DELETE FROM participante WHERE id = @Id", new { Id = id });
            }
        }
    }
}
