using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasLDCOM.Datos.Carrito.Response
{ 
    public class ResConsultaSocioFrecuente
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }

        public string Data { get; set; }
        public List<ConsultaSocioFrecuenteDetalle> DataL { get; set; }
    }

    public class ConsultaSocioFrecuenteDetalle
    {
        public string Correo { get; set; }
        public string Nombre { get; set; }
        public int Socio { get; set; }
        public string Tarjeta { get; set; }
        public string Telefono { get; set; }

        public ConsultaSocioFrecuenteDetalle(string correo, string nombre, int socio, string tarjeta, string telefono)
        {
            Correo = correo;
            Nombre = nombre;
            Socio = socio;
            Tarjeta = tarjeta;
            Telefono = telefono;
        }
    }
}
