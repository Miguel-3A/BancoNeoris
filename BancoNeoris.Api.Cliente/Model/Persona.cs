using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoNeoris.Api.Cliente.Model
{
    public class Persona
    {
        public Guid? personaId { get; set; }

        public string nombre { get; set; }

        public string genero { get; set; }

        public int edad { get; set; }

        public int identificacion { get; set; }

        public string direccion { get; set; }

        public int telefono { get; set; }
    }
}
