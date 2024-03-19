using System.Collections.Generic;

namespace Uniaraxa.AmigoSecreto.Core.Entities
{
    public class Participante
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public long LoginServiceId { get; set; }
        public List<ListaDeDesejos> ListasDesejos { get; set; }
        public Sorteio Sorteio { get; set; }
    }
}
