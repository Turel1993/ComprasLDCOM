using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasLDCOM.Datos.Carrito.BaseDeDatos
{
    public class TblSucursalClickCollect
    {
        [PrimaryKey, MaxLength(2)]
        public string Id { get; set; }
        public string Nombre { get; set; }
    }
}
