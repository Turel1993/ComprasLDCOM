using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasLDCOM.Datos.Carrito.Response
{ 
    public class ResSucursalUbicacion
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public List<SucursalUbicacionDetalle> Data { get; set; }
    }

    public class SucursalUbicacionDetalle
    {
        public int Emp_id { get; set; }
        public int Suc_id { get; set; }
        public int Caja_id { get; set; }


        public SucursalUbicacionDetalle(int emp_id, int suc_id, int caja_id)
        {
            Emp_id = emp_id;
            Suc_id = suc_id;
            Caja_id = caja_id;
        }
    }
}
