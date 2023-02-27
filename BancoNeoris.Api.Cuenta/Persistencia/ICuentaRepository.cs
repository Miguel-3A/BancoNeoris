using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoNeoris.Api.Cuenta.Persistencia
{
    public interface ICuentaRepository
    {
        Task<bool> Insert(Model.Cuenta cuenta);

        Task<IEnumerable<Model.Cuenta>> GetAll();

        Model.Cuenta GetById(string Id);

        void Update(Model.Cuenta cuenta);

        void DeleteById(string Id);

        IEnumerable<Model.Cuenta> GetByClienteId(string Id);
    }
}
