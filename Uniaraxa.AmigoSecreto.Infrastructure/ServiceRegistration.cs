using Microsoft.Extensions.DependencyInjection;
using Uniaraxa.AmigoSecreto.Infrastructure.Repository;
using Uniaraxa.AmigoSecreto.Application.Interfaces;

namespace Uniaraxa.AmigoSecreto.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IListaDeDesejosRepository, ListaDeDesejosRepository>();
            services.AddTransient<ILoginRepository, LoginRepository>();
            services.AddTransient<IParticipanteRepository, ParticipanteRepository>();
            services.AddTransient<ISorteioRepository, SorteioRepository>();
            services.AddScoped<IConvitesRepository, ConvitesRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
