using System;
using System.Collections.Generic;

namespace DataAccess
{
    public partial class Ingreso
    {
        public int IdIngreso { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaEgreso { get; set; }
        public int IdEmpresa { get; set; }
        public int IdVehiculo { get; set; }
        public int IdPersona { get; set; }
        public int IdTipoCarga { get; set; }
        public decimal? Peso { get; set; }

        public virtual Empresa IdEmpresaNavigation { get; set; } = null!;
        public virtual Persona IdPersonaNavigation { get; set; } = null!;
        public virtual TipoCarga IdTipoCargaNavigation { get; set; } = null!;
        public virtual Vehiculo IdVehiculoNavigation { get; set; } = null!;
    }
}
