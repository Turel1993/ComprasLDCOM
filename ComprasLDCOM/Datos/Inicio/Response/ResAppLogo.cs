using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasLDCOM.Datos.Inicio.Response
{
    public class ResAppLogo
    {
        public Boolean IsSuccessful { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
    }

    public class ResAppLogoData
    {
        public int? Id { get; set; }
        public int? Consecutivo { get; set; }
        public int? Emp_Id { get; set; }
        public int? Cadena_Id { get; set; }
        public string Tipo_Imagen { get; set; }
        public string PVTicket { get; set; }
        public string Imagen { get; set; }
    }
}
