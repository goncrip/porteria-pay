using System;
using System.Collections.Generic;

namespace DataAccess
{
    public partial class Persona
    {
        public Persona()
        {
            Ingresos = new HashSet<Ingreso>();
        }

        public int IdPersona { get; set; }
        public string TipoDocumento { get; set; } = null!;
        public string Documento { get; set; } = null!;
        public string Pais { get; set; } = null!;
        public string? Celular { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;

        public virtual ICollection<Ingreso> Ingresos { get; set; }
    }
}
