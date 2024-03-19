using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniaraxa.AmigoSecreto.Core.Entities
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string NomeDeUsuario { get; set; }
        public string Email { get; set; }
        public string SenhaHash { get; set; }
    }
}
