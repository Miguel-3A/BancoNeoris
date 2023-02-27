using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoNeoris.Api.Movimiento.Model
{
    public class Respuesta<T> where T : class
    {
        public bool resultado { get; set; }
        public T data { get; set; }
        public string mensaje { get; set; }
    }
}
