using System;
using System.Collections.Generic;

namespace DataAccess
{
    public partial class TipoCarga
    {
        public TipoCarga()
        {
            Ingresos = new HashSet<Ingreso>();
        }

        public int IdTipoCarga { get; set; }
        public string Descripcion { get; set; } = null!;

        public virtual ICollection<Ingreso> Ingresos { get; set; }
    }
}
