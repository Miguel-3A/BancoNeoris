using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoNeoris.Api.Cliente.Model
{
    public class Cliente : Persona
    {
        public Guid? clienteId { get; set; }

        public int contrasena { get; set; }

        public bool estado { get; set; }
    }
}
