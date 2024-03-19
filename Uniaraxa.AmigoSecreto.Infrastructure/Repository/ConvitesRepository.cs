using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Uniaraxa.AmigoSecreto.Application.Interfaces;

namespace Uniaraxa.AmigoSecreto.Infrastructure.Repository
{
    public class ConvitesRepository : IConvitesRepository
    {
        private readonly IConfiguration _configuration;
        private readonly Random _random;

        public ConvitesRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _random = new Random();
        }

        public async Task EnviarConviteEmail()
        {
            var smtpSettings = _configuration.GetSection("SmtpSettings");
            var smtpClient = new SmtpClient
            {
                Host = smtpSettings["Host"],
                Port = int.Parse(smtpSettings["Port"]),
                Credentials = new NetworkCredential(smtpSettings["Username"], smtpSettings["Password"]),
                EnableSsl = bool.Parse(smtpSettings["EnableSsl"])
            };

            // Obter o email de um participante sorteado aleatoriamente
            var destinatarioEmail = await ObterEmailParticipanteSorteadoAsync();

            var mailMessage = new MailMessage
            {
                From = new MailAddress(smtpSettings["Username"]),
                Subject = "Convite para Amigo Secreto",
                Body = $"Olá! Você foi sorteado para participar de um amigo secreto."
            };

            mailMessage.To.Add(new MailAddress(destinatarioEmail));

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
                Console.WriteLine($"Convite enviado para {destinatarioEmail}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar convite por e-mail: {ex.Message}");
                throw;
            }
        }

        private async Task<string> ObterEmailParticipanteSorteadoAsync()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new NpgsqlConnection(connectionString))
            {
                var participantes = await connection.QueryAsync<string>("SELECT email FROM participante");
                var emails = participantes.ToList();

                if (emails.Count == 0)
                {
                    throw new Exception("Não há participantes disponíveis para enviar convites.");
                }

                var indexSorteado = _random.Next(emails.Count);
                return emails[indexSorteado];
            }

        }

    }
}
