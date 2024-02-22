using System;
using System.Collections.Generic;

namespace DataAccess
{
    public partial class Vehiculo
    {
        public Vehiculo()
        {
            Ingresos = new HashSet<Ingreso>();
        }

        public int IdVehiculo { get; set; }
        public string Matricula { get; set; } = null!;
        public string Pais { get; set; } = null!;

        public virtual ICollection<Ingreso> Ingresos { get; set; }
    }
}
