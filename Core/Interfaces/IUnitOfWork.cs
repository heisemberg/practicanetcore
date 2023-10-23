using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUnitOfWork
    {
        IPais Paises {get;}
        IDepartamento Departamentos {get;}
        ICiudad Ciudades {get;}
        IDireccion Direcciones {get;}
        Task<int> SaveAsync();
    }
}