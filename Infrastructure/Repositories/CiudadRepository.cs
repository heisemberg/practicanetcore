using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class CiudadRepository : GenericRepository<Ciudad>, ICiudad
    {
        private readonly ApiContext _context;

        public CiudadRepository(ApiContext context) : base(context)
        {
            _context = context;
        }
    }
}