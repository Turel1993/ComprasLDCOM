using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasLDCOM.Datos.Cuenta.BaseDeDatos
{
    public class TblPerfil
    {
        [PrimaryKey]
        public string IdSocio { get; set; }
        public string Nombre { get; set; }
        //public string ApellidoPaterno { get; set; }
        //public string ApellidoMaterno { get; set; }
        public string Email { get; set; }
        //public int Lada { get; set; }
        public string Telefono { get; set; }
        //public string Genero { get; set; }
        public string FechaNacimiento { get; set; }
    }
}
