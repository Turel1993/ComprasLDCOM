using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasLDCOM.Datos.Inicio.BaseDeDatos
{
    public class TblFavoritos
    {
        [PrimaryKey]
        public string SkuArticulo { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
    }
}
