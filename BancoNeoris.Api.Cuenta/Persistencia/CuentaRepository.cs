using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoNeoris.Api.Cuenta.Persistencia
{
    public class CuentaRepository : ICuentaRepository
    {
        private readonly ContextoCuenta _contexto;
        public CuentaRepository(ContextoCuenta context)
        {
            _contexto = context;
        }

        public async Task<bool> Insert(Model.Cuenta cuenta)
        {
            _contexto.Cuenta.Add(cuenta);
            return await _contexto.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Model.Cuenta>> GetAll()
        {
            return await _contexto.Cuenta.ToListAsync();
        }

        public Model.Cuenta GetById(string cuentaId)
        {
            return _contexto.Cuenta.Find(new Guid(cuentaId));
        }

        public void Update(Model.Cuenta cuenta)
        {
            _contexto.Entry(cuenta).State = EntityState.Modified;
            _contexto.SaveChanges();
        }

        public void DeleteById(string cuentaId)
        {
            var cuenta = _contexto.Cuenta.Find(new Guid(cuentaId));
            if (cuenta != null)
            {
                _contexto.Cuenta.Remove(cuenta);
                _contexto.SaveChanges();
            }
        }

        public IEnumerable<Model.Cuenta> GetByClienteId(string clienteId)
        {
            return _contexto.Cuenta.Where(x => x.clienteId == new Guid(clienteId));
        }
    }
}
