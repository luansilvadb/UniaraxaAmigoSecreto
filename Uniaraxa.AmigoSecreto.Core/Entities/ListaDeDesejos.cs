using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniaraxa.AmigoSecreto.Core.Entities
{
    public class ListaDeDesejos
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public long ParticipanteId { get; set; }
    }

}
