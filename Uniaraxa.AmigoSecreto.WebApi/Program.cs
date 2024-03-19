using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;


namespace Uniaraxa.AmigoSecreto.WebApi
{
    /// <summary>
    /// Classe principal que contém o ponto de entrada para a execução do aplicativo.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Ponto de entrada para a execução do aplicativo.
        /// </summary>
        /// <param name="args">Os argumentos da linha de comando.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Cria um construtor de host para configurar e iniciar o aplicativo.
        /// </summary>
        /// <param name="args">Os argumentos da linha de comando.</param>
        /// <returns>O construtor de host.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
