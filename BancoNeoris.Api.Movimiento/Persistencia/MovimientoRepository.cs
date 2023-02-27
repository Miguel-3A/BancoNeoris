using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoNeoris.Api.Movimiento.Persistencia
{
    public class MovimientoRepository : IMovimientoRepository
    {
        private readonly ContextoMovimiento _contexto;
        public MovimientoRepository(ContextoMovimiento context)
        {
            _contexto = context;
        }

        public async Task<bool> Insert(Model.Movimiento movimiento)
        {
            _contexto.Movimiento.Add(movimiento);
            return await _contexto.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Model.Movimiento>> GetAll()
        {
            return await _contexto.Movimiento.ToListAsync();
        }

        public Model.Movimiento GetById(string movimientoId)
        {
            return _contexto.Movimiento.Find(new Guid(movimientoId));
        }

        public void Update(Model.Movimiento movimiento)
        {
            _contexto.Entry(movimiento).State = EntityState.Modified;
            _contexto.SaveChanges();
        }

        public void DeleteById(string movimientoId)
        {
            var movimiento = _contexto.Movimiento.Find(new Guid(movimientoId));
            if (movimiento != null)
            {
                _contexto.Movimiento.Remove(movimiento);
                _contexto.SaveChanges();
            }
        }

        public IEnumerable<Model.Movimiento> FiltroMovimientos(string cuentaId, DateTime fechaInicial, DateTime fechaFinal)
        {
            return _contexto.Movimiento.Where(x => x.cuentaId == new Guid(cuentaId) && x.fechaMovimiento >= fechaInicial && x.fechaMovimiento <= fechaFinal).ToList();
        }
    }
}
