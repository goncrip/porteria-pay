using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PorteriaFunction.DTO
{
    public class IngresoDTO
    {
        public int IdEmpresa { get; set; }
        public int IdVehiculo { get; set; }
        public int IdPersona { get; set; }
        public int IdTipoCarga { get; set; }
        public decimal Peso { get; set; }
    }
}
