using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class DireccionRepository : GenericRepository<Direccion>, IDireccion
    {
        private readonly ApiContext _context;

        public DireccionRepository(ApiContext context) : base(context)
        {
            _context = context;
        }
    }
}