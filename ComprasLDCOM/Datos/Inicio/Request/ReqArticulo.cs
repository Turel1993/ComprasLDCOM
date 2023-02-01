using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasLDCOM.Datos.Inicio.Request
{ 
    public class ReqArticulo
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public List<Articulo> Data { get; set; }
    }

    public class Articulo
    {
        public string Sku { get; set; }
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
        public string NombrePromocion { get; set; }
        public ImageSource Imagen { get; set; }
        public string Imagen64 { get; set; }
        public bool IsVisibleAddCart { get; set; }
        public string ColorDisponible { get; set; }
        public int Piezas { get; set; }
        public decimal TotalProducto { get; set; }
        public decimal TotalDescuento { get; set; }
        public decimal TotalUnitario { get; set; }
        public decimal SubTotalProducto { get; set; }
        public List<ReqInfoArt> Caracteristicas { get; set; }
        public bool Tiene_Serie { get; set; }
        public bool Tiene_Caducidad { get; set; }
        public string Articulo_Suelto { get; set; }
        public decimal Costo_Unitario { get; set; }
        public string Articulo_Regalo { get; set; }
        public bool IsVisibleIncremento { get; set; }
        public decimal Promocion_Base { get; set; }
        public decimal Promocion_Regalo { get; set; }
        public bool IsVisiblePiezasPromocion { get; set; }
        public bool IsVisibleLowCostGif { get; set; }
        public bool Controlado { get; set; }
        public string Lote_Caducidad { get; set; }
        public decimal Descuento_Porc_Lote { get; set; }
        public bool IsVisibleLote { get; set; }
        public ImageSource Imagen_BajoCosto { get; set; }
        public string Imagen64_BajoCosto { get; set; }
        public bool IsVisibleDescuentoTexto { get; set; }
        public bool IsVisibleTotalUnitario { get; set; }
        public int RowSpanIsvisibleDescuento { get; set; }
        public int RowIsvisibleDescuento { get; set; }
        public ImageSource ImagenFavoritos { get; set; }

        public Articulo(string sku, string nombre, decimal precio, decimal precioMaximo, decimal precioUnitario, decimal descuentoMonto, decimal descuentomonto_origen, decimal descuentoPorcentaje, decimal descuentoPorcentaje_origen, string nombreDescuento,
                        decimal ivaMonto, decimal ivaPorcentaje, decimal iepsMonto, decimal iepsPorcentaje, int cantDisponible, string nombrepromocion, ImageSource imagen,
                        string img64, int piezas, decimal totalProducto, decimal totalDescuento, decimal totalUnitario, decimal subTotalProducto, List<ReqInfoArt> caracteristicas,
                        bool tiene_serie, bool tiene_caducidad, string articulo_suelto, decimal costo_unitario, string articulo_regalo, bool isvisibleincremento, decimal promocion_Base,
                        decimal promocion_Regalo, bool isVisibleLowCostGif, bool cotrolado, string lote_caducidad, decimal descuento_porc_lote, bool isvisiblelote, ImageSource imagen_BajoCosto,
                        string img64_BajoCosto, ImageSource imagenFavoritos)
        {
            Sku = (sku == null ? "" : sku);
            Nombre = (nombre == null ? "" : nombre);
            Precio = precio;
            PrecioMaximo = precioMaximo;
            PrecioUnitario = precioUnitario;
            DescuentoMonto = descuentoMonto;
            DescuentoMonto_Origen = descuentomonto_origen;
            DescuentoPorcentaje = descuentoPorcentaje;
            DescuentoPorcentaje_Origen = descuentoPorcentaje_origen;
            NombreDescuento = (nombreDescuento == null ? "" : nombreDescuento);
            IvaMonto = ivaMonto;
            IvaPorcentaje = ivaPorcentaje;
            IepsMonto = iepsMonto;
            IepsPorcentaje = iepsPorcentaje;
            CantDisponible = cantDisponible;
            NombrePromocion = (nombrepromocion == null ? "" : nombrepromocion);
            Imagen = imagen;
            Imagen64 = img64;
            IsVisibleAddCart = (cantDisponible == 0 ? true : true);
            ColorDisponible = (cantDisponible == 0 ? "#D60000" : "#20D600");
            Piezas = piezas;
            TotalProducto = totalProducto;
            TotalDescuento = totalDescuento;
            TotalUnitario = totalUnitario;
            SubTotalProducto = subTotalProducto;
            Caracteristicas = caracteristicas;
            Tiene_Serie = tiene_serie;
            Tiene_Caducidad = tiene_caducidad;
            Articulo_Suelto = articulo_suelto;
            Costo_Unitario = costo_unitario;
            Articulo_Regalo = (articulo_regalo == null ? "" : articulo_regalo);
            IsVisibleIncremento = isvisibleincremento;
            Promocion_Base = promocion_Base;
            Promocion_Regalo = promocion_Regalo;
            IsVisiblePiezasPromocion = !isvisibleincremento;
            IsVisibleLowCostGif = isVisibleLowCostGif;
            Controlado = cotrolado;
            Lote_Caducidad = lote_caducidad;
            Descuento_Porc_Lote = descuento_porc_lote;
            IsVisibleLote = isvisiblelote;
            Imagen_BajoCosto = imagen_BajoCosto;
            Imagen64_BajoCosto = img64_BajoCosto;
            IsVisibleDescuentoTexto = (descuentomonto_origen > 0 && descuentoPorcentaje_origen > 0) ? true : false;
            IsVisibleTotalUnitario = IsVisibleDescuentoTexto;
            RowIsvisibleDescuento = IsVisibleTotalUnitario ? 4 : 3;
            RowSpanIsvisibleDescuento = IsVisibleTotalUnitario ? 1 : 2;
            ImagenFavoritos = imagenFavoritos;
        }
    }
}
