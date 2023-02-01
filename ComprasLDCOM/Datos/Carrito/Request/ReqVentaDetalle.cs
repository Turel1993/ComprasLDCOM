using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasLDCOM.Datos.Carrito.Request
{
    public class ReqVentaDetalle
    {
        /// <summary>
        /// Id del artículo Ej: 4000164
        /// </summary>
        public string Articulo_Id { get; set; }
        /// <summary>
        /// Cantidad de la unidades del articulo Ej: 1.000
        /// </summary>
        public int Detalle_Cantidad { get; set; }
        /// <summary>
        /// Precio Unitario Ej: 895.00
        /// </summary>
        public float Detalle_Precio_Unitario { get; set; }
        /// <summary>
        /// Monto descuento unitario Ej: 107.40
        /// </summary>
        public decimal Detalle_Descuento_Monto { get; set; }
        /// <summary>
        /// Porcentaje de Descuento Ej: 12.00
        /// </summary>
        public decimal Detalle_Descuento_Porc { get; set; }
        /// <summary>
        /// Codigo del Lote Ej. 20076512
        /// </summary>
        public string Detalle_Lote { get; set; }
        /// <summary>
        /// Detalle Total Ej. 100.00
        /// </summary>
        public decimal Detalle_Total { get; set; }
        /// <summary>
        /// Detalle IVA Monto Ej. 100.00
        /// </summary>
        public decimal Detalle_IVA_Monto { get; set; }
        /// <summary>
        /// Detalle IVA Porcentaje Ej. 50.00
        /// </summary>
        public decimal Detalle_IVA_Porc { get; set; }
        /// <summary>
        /// Detalle IEPS Monto Ej. 100.00
        /// </summary>
        public decimal Detalle_IEPS_Monto { get; set; }
        /// <summary>
        /// Detalle IEPS Porcentaje Ej. 50.00
        /// </summary>
        public decimal Detalle_IEPS_Porc { get; set; }
        /// <summary>
        /// Tipo de Precio Ej: 1
        /// </summary>
        public int Detalle_Tipo_Precio { get; set; }


        public ReqVentaDetalle(string articulo_Id, int detalle_Cantidad, float detalle_Precio_Unitario, decimal detalle_Descuento_Monto, decimal detalle_Descuento_Porc, string detalle_Lote, decimal detalle_Total, decimal detalle_IVA_Monto, decimal detalle_IVA_Porc, decimal detalle_IEPS_Monto, decimal detalle_IEPS_Porc, int detalle_Tipo_Precio)
        {
            Articulo_Id = articulo_Id;
            Detalle_Cantidad = detalle_Cantidad;
            Detalle_Precio_Unitario = detalle_Precio_Unitario;
            Detalle_Descuento_Monto = detalle_Descuento_Monto;
            Detalle_Descuento_Porc = detalle_Descuento_Porc;
            Detalle_Lote = detalle_Lote;
            Detalle_Total = detalle_Total;
            Detalle_IVA_Monto = detalle_IVA_Monto;
            Detalle_IVA_Porc = detalle_IVA_Porc;
            Detalle_IEPS_Monto = detalle_IEPS_Monto;
            Detalle_IEPS_Porc = detalle_IEPS_Porc;
            Detalle_Tipo_Precio = detalle_Tipo_Precio;
        }
    }
}
