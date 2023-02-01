using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasLDCOM.Modelos.Carrito
{
    internal class BonificacionMonederoFormaPago
    { /// <summary>
      /// Identificador de la forma de pago Ej: 1
      /// </summary>
        public int FormaPago_Id { get; set; }

        /// <summary>
        /// Monto Total de la Venta Ej: 1234
        /// </summary>
        public string FormaPago_Monto { get; set; }
    }
}
