using System;

namespace Uniaraxa.AmigoSecreto.Core.Entities
{
    public class Sorteio
    {
        public long Id { get; set; }
        public DateTime DataSorteio { get; set; }
        public long ParticipanteSorteadoId { get; set; }
    }
}
