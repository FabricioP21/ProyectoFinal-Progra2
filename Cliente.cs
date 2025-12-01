using System;
using System.Collections.Generic;
using System.Text;

namespace Proy_Fin
{
  public class Cliente : Persona
    {
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public List<Animal> Animales { get; set; } = new List<Animal>();
        public List<HistorialClinico> HistorialClinico { get; set; } = new List<HistorialClinico>();

        public override string ObtenerTipo() => "Cliente";
    }
}
