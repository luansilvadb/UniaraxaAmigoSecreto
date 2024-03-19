using System.Threading.Tasks;

namespace Uniaraxa.AmigoSecreto.Application.Interfaces
{
    public interface IConvitesRepository
    {
        Task EnviarConviteEmail();
    }
}
