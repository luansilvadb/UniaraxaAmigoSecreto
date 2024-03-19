using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uniaraxa.AmigoSecreto.Application.Interfaces;
using Uniaraxa.AmigoSecreto.Core.Entities;

namespace Uniaraxa.AmigoSecreto.Infrastructure.Repository
{
    public class SorteioRepository : ISorteioRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IConvitesRepository _convitesRepository;
        private readonly string _connectionString;

        public SorteioRepository(IConfiguration configuration, IConvitesRepository convitesRepository)
        {
            _configuration = configuration;
            _convitesRepository = convitesRepository;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IReadOnlyList<Sorteio>> ObterTodosAsync()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<Sorteio>("SELECT * FROM sorteio");
                return result.AsList().AsReadOnly();
            }
        }

        public async Task<Sorteio> ObterPorIdAsync(long id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.QueryFirstOrDefaultAsync<Sorteio>("SELECT * FROM sorteio WHERE id = @Id", new { Id = id });
            }
        }

        public async Task<long> InserirAsync(Sorteio entity)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                // Obter um ID de participante aleatório da tabela participante
                var participanteSorteadoId = await ObterIdParticipanteAleatorio(connection);

                // Prepara a consulta SQL com parâmetros nomeados
                var sql = "INSERT INTO sorteio (data_sorteio, id_participante_sorteado) VALUES (@DataSorteio, @ParticipanteSorteadoId) RETURNING id";

                // Executa a consulta SQL e obtém o ID do novo sorteio inserido
                var idSorteio = await connection.ExecuteScalarAsync<long>(sql, new
                {
                    entity.DataSorteio,
                    ParticipanteSorteadoId = participanteSorteadoId
                });

                // Enviar convite por e-mail ao participante sorteado
                await _convitesRepository.EnviarConviteEmail();

                return idSorteio;
            }
        }

        public async Task<long> AtualizarAsync(Sorteio entity)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.ExecuteAsync("UPDATE sorteio SET data_sorteio = @DataSorteio, id_participante_sorteado = @ParticipanteSorteadoId WHERE id = @Id", entity);
            }
        }

        public async Task<long> RemoverAsync(long id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.ExecuteAsync("DELETE FROM sorteio WHERE id = @Id", new { Id = id });
            }
        }
        private async Task<long> ObterIdParticipanteAleatorio(NpgsqlConnection connection)
        {
            // Prepara e executa a consulta para obter um ID de participante aleatório
            var participanteSorteadoId = await connection.ExecuteScalarAsync<long>("SELECT id FROM participante ORDER BY random() LIMIT 1");
            return participanteSorteadoId;
        }
    }
}
