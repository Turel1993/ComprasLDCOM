using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasLDCOM.Datos.Configuraciones.BaseDeDatos
{
    public class TblImagenes
    {
        [PrimaryKey]
        public string Nombre { get; set; }
        [MaxLength(100000)]
        public string Imagen { get; set; }
    }
}
