using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Ciudad
    {
        public string NombreCiudad { get; set; }
        public int DepartamentoId { get; set; }
        public Departamento Departamentos { get; set; }
        public ICollection<Direccion> Direcciones { get; set; }
    }
}