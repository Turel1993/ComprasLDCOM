using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasLDCOM.Datos.Cuenta.BaseDeDatos
{
    public class TblSesion
    {
        [PrimaryKey]
        public string IdSocio { get; set; }
        public string Usuario_Id { get; set; }
        public string Password { get; set; }
        public string Token { get ; set; }
        public string Caja { get; set; }
        public int Cliente { get; set; }
    }
}
