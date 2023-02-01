using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasLDCOM.Datos.Inicio.Request
{
    public class ReqInfoArt 
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }

        public ReqInfoArt(string nombre, string descripcion, int orden)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            Orden = orden;
        }
    }
}
