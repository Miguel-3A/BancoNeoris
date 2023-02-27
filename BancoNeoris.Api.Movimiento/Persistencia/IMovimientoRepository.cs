using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoNeoris.Api.Movimiento.Persistencia
{
    public interface IMovimientoRepository
    {
        Task<bool> Insert(Model.Movimiento movimiento);

        Task<IEnumerable<Model.Movimiento>> GetAll();

        Model.Movimiento GetById(string Id);

        void Update(Model.Movimiento movimiento);

        void DeleteById(string Id);

        IEnumerable<Model.Movimiento> FiltroMovimientos(string cuentaId, DateTime fechaInicial, DateTime fechaFinal);
    }
}
