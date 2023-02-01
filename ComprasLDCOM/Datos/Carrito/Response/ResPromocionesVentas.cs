using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasLDCOM.Datos.Carrito.Response
{
    internal class ResPromocionesVentas
    {
        public Boolean IsSuccessful { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
        public Table Detalle { get; set; }
    }

    public class Table
    {
        public Boolean Exitoso { get; set; }
        public string Mensaje { get; set; }
        public decimal Subtotal { get; set; }
        public decimal DescuentoMonto { get; set; }
        public decimal IVA { get; set; }
        public decimal IEPS { get; set; }
        public decimal Cupon { get; set; }
        public decimal total_Pagar { get; set; }
        public int Prefactura_Id { get; set; }
    }
}
