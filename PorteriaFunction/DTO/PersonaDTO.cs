using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PorteriaFunction.DTO
{
    public class PersonaDTO
    {
        public int IdPersona { get; set; }
        public string TipoDocumento { get;set; }
        public string Documento { get; set; }
        public string Pais { get; set; }
        public string Celular { get;set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set;}
    }
}
