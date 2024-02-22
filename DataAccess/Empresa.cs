using System;
using System.Collections.Generic;

namespace DataAccess
{
    public partial class Empresa
    {
        public Empresa()
        {
            Ingresos = new HashSet<Ingreso>();
        }

        public int IdEmpresa { get; set; }
        public string? Nombre { get; set; }

        public virtual ICollection<Ingreso> Ingresos { get; set; }
    }
}
