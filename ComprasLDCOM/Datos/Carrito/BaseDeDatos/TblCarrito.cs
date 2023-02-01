using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasLDCOM.Datos.Carrito.BaseDeDatos
{ 
    public class TblCarrito
    {
        public string IdCarrito { get; set; }

        [PrimaryKey,MaxLength(20)]
        public string SkuArticulo { get; set; }
        [MaxLength(100)]
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public decimal PrecioMaximo { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal DescuentoMonto { get; set; }
        public decimal DescuentoMonto_Origen { get; set; }
        public decimal DescuentoPorcentaje { get; set; }
        public decimal DescuentoPorcentaje_Origen { get; set; }
        public string NombreDescuento { get; set; }
        public decimal IvaMonto { get; set; }
        public decimal IvaPorcentaje { get; set; }
        public decimal IepsMonto { get; set; }
        public decimal IepsPorcentaje { get; set; }
        public int CantDisponible { get; set; }
        [MaxLength(60)]
        public string NombrePromocion { get; set; }
        [MaxLength(20000)]
        public string Imagen { get; set; }
        public int Piezas { get; set; }
        public decimal TotalProducto { get; set; }
        public decimal TotalDescuento { get; set; }
        public decimal TotalUnitario { get; set; }
        public decimal SubTotalProducto { get; set; }
        public bool Tiene_Serie { get; set; }
        public bool Tiene_Caducidad { get; set; }
        public string Lote_Caducidad { get; set; }
        public string Articulo_Suelto { get; set; }
        public decimal Costo_Unitario { get; set; }
        public string Articulo_Regalo { get; set; }
        public bool IsVisiableIncremento { get; set; }
        public decimal Promocion_Base { get; set; }
        public decimal Promocion_Regalo { get; set; }
        public bool IsVisiblePiezasPromocion { get; set; }
        public bool IsVisibleLowCostGif { get; set; }
        public bool Controlado { get; set; }
        public decimal Descuento_Porc_Lote { get; set; }
        public bool IsVisibleLote { get; set; }
        [MaxLength(20000)]
        public string Imagen_BajoCosto { get; set; }
    }
}
