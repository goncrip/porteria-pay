using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PorteriaFunction.DTO
{
    public class ConsultaIngresoDTO
    {
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaEgreso { get; set; }
        public string Empresa { get; set; }
        public string Matricula { get; set; }
        public string PaisMatricula { get;set; }
        public string Documento { get; set; }
        public string Nombres { get;set; }
        public string Apellidos { get; set; }
        public string TipoCarga { get; set; }
        public decimal Peso { get; set; }
    }
}
