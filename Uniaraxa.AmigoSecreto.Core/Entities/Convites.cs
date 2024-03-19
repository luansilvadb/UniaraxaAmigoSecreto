using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniaraxa.AmigoSecreto.Core.Entities
{
    public class Convites
    {
        public long ConviteId { get; set; }
        public long RemetenteId { get; set; }
        public string DestinatarioEmail { get; set; }
        public string Status { get; set; }
        public DateTime CriadoEm { get; set; }
    }
}
