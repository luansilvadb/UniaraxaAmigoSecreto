namespace Uniaraxa.AmigoSecreto.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IListaDeDesejosRepository ListaDeDesejos { get; }
        ILoginRepository Login { get; }
        IParticipanteRepository Participante { get; }
        ISorteioRepository Sorteio { get; }
        IConvitesRepository Convites { get; }
    }
}
