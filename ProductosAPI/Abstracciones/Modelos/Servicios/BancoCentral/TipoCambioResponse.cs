using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos.Servicios.BancoCentral
{
    public class TipoCambioResponse
    {
        public bool Estado { get; set; }
        public string Mensaje { get; set; }
        public List<Dato> Datos { get; set; }


    }

    public class Dato
    {
        public string Titulo { get; set; }
        public string Periodicidad { get; set; }
        public List<Indicador> Indicadores { get; set; }
    }

    public class Indicador
    {
        public string CodigoIndicador { get; set; }
        public string NombreIndicador { get; set; }
        public List<Serie> Series { get; set; }
    }

    public class Serie
    {
        public string Fecha { get; set; }
        public decimal ValorDatoPorPeriodo { get; set; }
    }
}
