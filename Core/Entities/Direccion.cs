using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Direccion
    {
        public string DetalleDireccion { get; set; }
        public int CiudadId { get; set; }
        public Ciudad Ciudades { get; set; }
    }
}