using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasLDCOM.Datos.Configuraciones.BaseDeDatos
{
    public class TblConfiguracion
    {
        [PrimaryKey]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Nombre { get; set; }
        [MaxLength(100)]
        public string Descripcion { get; set; }
        [MaxLength(1000)]
        public string Valor { get; set; }
    }
}
