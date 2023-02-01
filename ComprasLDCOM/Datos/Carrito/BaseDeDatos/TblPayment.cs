using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasLDCOM.Datos.Carrito.BaseDeDatos
{
    public class TblPayment
    {
        public int IdCarrito { get; set; }

        [PrimaryKey, MaxLength(2)]
        public string TipoPago { get; set; }

        public string MontoPagado { get; set; }

        public string MontoCapturado { get; set; }

        public string TotalMontoCapturado { get; set; }

        public int IdBanco { get; set; }

        public string NombreBanco { get; set; }

        public string NumTarjeta { get; set; }

        public string NumVoucher { get; set; }

        public int Ticket { get; set; }
    }
}
