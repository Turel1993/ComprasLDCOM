using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasLDCOM.Datos.Carrito.BaseDeDatos
{  
    public class TblGeneralesVenta
    {
        [PrimaryKey, MaxLength(10)]
        public int IdUsuario { get; set; }
        /// <summary>
        /// Datos obtenidos de la Api TipoDocumentoLista
        /// 1:Factura Contado,   2:Apartado,   3:Devolucion,   4:Factura Credito
        /// 5:Devolucion Cedi,   6:Cobro Servicio,   7:Anula Apartado,   8:Servicio Externo
        /// </summary>
        public string IdTipoDocumento { get; set; }
        /// <summary>
        /// Nombre del Tipo de Documento
        /// </summary>
        public string NombreDocumento { get; set; }
        /// <summary>
        /// Datos obtenidos de la Api Cliente_Lista
        /// 10000001: Mostrador Público en General
        /// </summary>
        public string IdTipoCliente { get; set; }
        /// <summary>
        /// Nombre del Tipo Cliente
        /// </summary>
        public string NombreCliente { get; set; }  
        /// <summary>
        /// Número del Monedero ingresado
        /// </summary>
        public string NumMonedero { get; set; }
        /// <summary>
        /// Saldo del Monedero ingresado
        /// </summary>
        public decimal SaldoMonedero { get; set; }
        /// <summary>
        /// Id del Socio del Monedero ingresado
        /// </summary>
        public int IdSocioMonedero { get; set; }
        public string PTarjeta { get; set; }
        public string Transaccion { get; set; }
        /// <summary>
        /// Direccción seleccionada para la compra
        /// </summary>
        public string Direccion { get; set; }
        /// <summary>
        /// Id de la Sucursal seleccionada para Click_Collect
        /// </summary>
        public string SucursalClickCollect_Id { get; set; }
        /// <summary>
        /// Nombre de la Sucursal seleccionada para Click_Collect
        /// </summary>
        public string SucursalClickCollect_Nombre { get; set; }

    }
}
