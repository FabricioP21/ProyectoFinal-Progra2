using System;
using System.Collections.Generic;
using System.Text;

namespace Proy_Fin
{
    public abstract class Persona
    {
        public string Apellido { get; set; }
        public string CI { get; set; }
        public abstract string ObtenerTipo();
    }

}
