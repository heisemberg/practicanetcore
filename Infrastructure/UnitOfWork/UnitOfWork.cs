using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApiContext _context;

        private IPais _paises;
        private IDepartamento _departamentos;
        private ICiudad _ciudades;
        private IDireccion _direcciones;

        public UnitOfWork(ApiContext context)
        {
            _context = context;
        }

public IPais Paises{
    get{
        if(_paises == null){
            _paises = new PaisRepository(_context);
        }
        return _paises;
    }
}

public IDepartamento Departamentos{
    get{
        if(_departamentos == null){
            _departamentos = new DepartamentoRepository(_context);
        }
        return _departamentos;
    }
}

public ICiudad Ciudades{
    get{
        if(_ciudades == null){
            _ciudades = new CiudadRepository(_context);
        }
        return _ciudades;
    }
}

public IDireccion Direcciones{
    get{
        if(_direcciones == null){
            _direcciones = new DireccionRepository(_context);
        }
        return _direcciones;
    }
}
        

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}