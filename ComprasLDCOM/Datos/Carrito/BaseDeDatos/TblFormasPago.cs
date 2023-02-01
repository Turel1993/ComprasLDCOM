using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasLDCOM.Datos.Carrito.BaseDeDatos
{
    public class TblFormasPago
    {
        public int IdCarrito { get; set; }

        [PrimaryKey, MaxLength(2)]
        public string Tipo_Id { get; set; }
        public string Tipo_NombrePago { get; set; }

        public string Pago_Total { get; set; }

        public string TotalMontoCapturado { get; set; }
    }
}