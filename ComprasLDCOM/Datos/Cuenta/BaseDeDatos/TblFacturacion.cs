using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasLDCOM.Datos.Cuenta.BaseDeDatos
{
    public class TblFacturacion
    {
        [PrimaryKey]
        public string IdSocio { get; set; }
        public string RFC { get; set; }
        public string RazonSocial { get; set; }
        public string DireccionFiscal { get; set; }
        public string CodigoPostal { get; set; }
        public string RegimenFiscal { get; set; }
        public string UsoCFDI { get; set; }
    }
}
