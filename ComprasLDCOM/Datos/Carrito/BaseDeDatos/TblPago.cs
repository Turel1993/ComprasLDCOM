using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasLDCOM.Datos.Carrito.BaseDeDatos
{
    internal class TblPago
    {
        public int Id { get; set; }
        public string TipoPago { get; set; }
        public float Monto { get; set; }

        public int TamanoDetalle { get; set; }
        public int TamanoRowDetalleCollection { get; set; }

        public TblPago(int id, string tipoPago, float monto, int tamanoDetalle, int tamanoRowDetalleCollection)
        {
            Id = id;
            TipoPago = tipoPago;
            Monto = monto;
            TamanoDetalle = tamanoDetalle;
            TamanoRowDetalleCollection = tamanoRowDetalleCollection;
        }
    }
}
