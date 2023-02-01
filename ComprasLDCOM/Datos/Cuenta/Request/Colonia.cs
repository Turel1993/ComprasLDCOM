using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasLDCOM.Datos.Cuenta.Response
{
    public class Colonia
    {
        public int Emp_id { get; set; }
        public int Usuario { get; set; }
        public int Nivel1_id { get; set; }
        public int Nivel2_id { get; set; }
        public int Nivel3_id { get; set; }
        public string Nombre { get; set; }
        public string Titulo_Nivel1 { get; set; }
        public string Titulo_Nivel2 { get; set; }
        public string Titulo_Nivel3 { get; set; }
        public string CodigoPostal { get; set; }
        public bool Nivel3_Ecommerce { get; set; }
        public string Clave { get; set; }
    }
}
