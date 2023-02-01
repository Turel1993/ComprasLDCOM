using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasLDCOM.Datos.Carrito.BaseDeDatos
{
    public class TblArticulosCupones
    {
        [PrimaryKey]
        public string Articulo { get; set; }
        public decimal Monto { get; set; }
    }
}
