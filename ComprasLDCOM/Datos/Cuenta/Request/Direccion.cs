using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasLDCOM.Datos.Cuenta.Request
{
    public class Direccion
    {
        public int Id { get; set; }
        public string Alias { get; set; }
        public string Calle { get; set; }
        public string Ciudad { get; set; }
    }
}
