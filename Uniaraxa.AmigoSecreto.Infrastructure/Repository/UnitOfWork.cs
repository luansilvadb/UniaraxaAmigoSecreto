using Uniaraxa.AmigoSecreto.Application.Interfaces;
using Uniaraxa.AmigoSecreto.Core.Entities;

namespace Uniaraxa.AmigoSecreto.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        public UnitOfWork(ILoginRepository loginRepository,
                          IListaDeDesejosRepository listadeDesejosRepository,
                          IParticipanteRepository participanteRepository,
                          ISorteioRepository sorteioRepository,
                          IConvitesRepository convitesRepository)
        {

            Login = loginRepository;
            ListaDeDesejos = listadeDesejosRepository;
            Participante = participanteRepository;
            Sorteio = sorteioRepository;
            Convites = convitesRepository;
        }


        public IListaDeDesejosRepository ListaDeDesejos { get; }
        public ILoginRepository Login { get; }
        public IParticipanteRepository Participante { get; }
        public ISorteioRepository Sorteio { get; }
        public IConvitesRepository Convites { get; }
    }
}
