using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoNeoris.Api.Cliente.Persistencia
{
    public interface IClienteRepository
    {
        Task<bool> Insert(Model.Cliente cliente);

        Task<IEnumerable<Model.Cliente>> GetAll();

        Model.Cliente GetById(string Id);        

        void Update(Model.Cliente cliente);

        void DeleteById(string Id);

        Model.Cliente GetByIdentificacion(int identificacion);
    }
}
