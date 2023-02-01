using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasLDCOM.Datos.Cuenta.BaseDeDatos
{
    public class TblTerminos
    {
        [PrimaryKey]
        public string IdTerminos { get; set; }
        [MaxLength(100000)]
        string AvisoProvacidad {  get; set; }
        [MaxLength(100000)]
        string TerminosCondiciones { get; set; }
    }
}
