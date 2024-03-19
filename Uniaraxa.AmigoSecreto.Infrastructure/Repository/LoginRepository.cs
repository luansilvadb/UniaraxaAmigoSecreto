using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Uniaraxa.AmigoSecreto.Application.Interfaces;
using Uniaraxa.AmigoSecreto.Core.Entities;
using Uniaraxa.AmigoSecreto.Core.Validador;

namespace Uniaraxa.AmigoSecreto.Infrastructure.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly string _connectionString;

        public LoginRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IReadOnlyList<Login>> ObterTodosAsync()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<Login>("SELECT * FROM login");
                return result.AsList().AsReadOnly();
            }
        }

        public async Task<Login> ObterPorIdAsync(long id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.QueryFirstOrDefaultAsync<Login>("SELECT * FROM login WHERE id = @Id", new { Id = id });
            }
        }

        public async Task<Login> ObterPorEmailAsync(string email)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.QueryFirstOrDefaultAsync<Login>("SELECT * FROM login WHERE email = @Email", new { Email = email });
            }
        }

        public async Task<long> InserirAsync(Login entity)
        {
            var emailValido = new ValidadorEmail(entity.Email).EmailEValido();
            if (!emailValido)
            {
                throw new ArgumentException("O e-mail especificado não está em um formato válido.");
            }

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var existingUser = await ObterPorEmailAsync(entity.Email);
                if (existingUser != null)
                {
                    // Efetuar login caso o usuário já exista
                    if (existingUser.Senha == entity.Senha)
                    {
                        return existingUser.Id; // Retornar o ID do usuário existente
                    }
                    else
                    {
                        throw new Exception("Senha incorreta. Tente novamente.");
                    }
                }
                else
                {
                    // Inserir novo usuário caso não exista
                    await connection.ExecuteAsync("INSERT INTO login (nome, email, senha) VALUES (@Nome, @Email, @Senha)", entity);
                    return entity.Id; // Retornar o ID do usuário inserido
                }
            }
        }




        public async Task<long> AtualizarAsync(Login entity)
        {
            var emailValido = new ValidadorEmail(entity.Email).EmailEValido();
            if (!emailValido)
            {
                throw new ArgumentException("O e-mail especificado não está em um formato válido.");
            }

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.ExecuteAsync("UPDATE login SET nome = @Nome, email = @Email, senha = @Senha WHERE id = @Id", entity);
            }
        }

        public async Task<long> RemoverAsync(long id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.ExecuteAsync("DELETE FROM login WHERE id = @Id", new { Id = id });
            }
        }
    }
}
