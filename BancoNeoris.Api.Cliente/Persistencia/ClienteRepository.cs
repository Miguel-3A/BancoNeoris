using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoNeoris.Api.Cliente.Persistencia
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ContextoCliente _contexto;
        public ClienteRepository(ContextoCliente context)
        {
            _contexto = context;
        }

        public async Task<bool> Insert(Model.Cliente cliente)
        {
            _contexto.Cliente.Add(cliente);
            return await _contexto.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Model.Cliente>> GetAll()
        {
            return await _contexto.Cliente.ToListAsync();
        }

        public Model.Cliente GetById(string clienteId)
        {
            return _contexto.Cliente.Find(new Guid(clienteId));
        }

        public void Update(Model.Cliente cliente)
        {
            _contexto.Entry(cliente).State = EntityState.Modified;
            _contexto.SaveChanges();
        }

        public void DeleteById(string clienteId)
        {
            var cliente = _contexto.Cliente.Find(new Guid(clienteId));
            if (cliente != null)
            {
                _contexto.Cliente.Remove(cliente);
                _contexto.SaveChanges();
            }
        }

        public Model.Cliente GetByIdentificacion(int identificacion)
        {
            return _contexto.Cliente.Where(x=>x.identificacion == identificacion).FirstOrDefault();
        }
    }
}
