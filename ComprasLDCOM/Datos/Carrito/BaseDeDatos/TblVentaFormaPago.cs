using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasLDCOM.Datos.Carrito.BaseDeDatos
{
    public class TblVentaFormaPago
    {
        /// <summary>
        /// Identificador de la forma de pago Ej: 2
        /// </summary>
        [PrimaryKey, MaxLength(2)]
        public int Tipo_Id { get; set; }
        /// <summary>
        /// Numero de pago, según regla se llena Ej: 1515
        /// </summary>
        public string Pago_Numero { get; set; }
        /// <summary>
        /// Aprobación de pago, según regla se llena Ej: 55
        /// </summary>
        public string Pago_Aprovacion { get; set; }
        /// <summary>
        /// Identificador de la moneda Ej: 1
        /// </summary>
        public int Moneda_Id { get; set; }
        /// <summary>
        /// Identificador del Banco Ej: 0
        /// </summary>
        public int Banco_Id { get; set; }
        /// <summary>
        /// Numero de Monedero Ej: ""
        /// </summary>
        public string MonederoTarj_Id { get; set; }
        /// <summary>
        /// Identificador de la tarjeta Ej: 2
        /// </summary>
        public int Tarjeta_Id { get; set; }
        /// <summary>
        /// Monto del Pago Ej: 2106.93
        /// </summary>
        public double Pago_Total { get; set; }
        /// <summary>
        /// Monto del pago, si fuese diferente moneda cambia del total Ej: 2106.93
        /// </summary>
        public double Pago_Total_Moneda { get; set; }
        /// <summary>
        /// Tipo de cambio, si fuese diferente moneda se agrega Ej: 1
        /// </summary>
        public int Pago_Total_Tipo_Cambio { get; set; }
        /// <summary>
        /// Identificador de la sucursal de referencia cuando es una Nota de crédito Ej: 0
        /// </summary>
        public int Pago_Suc_Ref { get; set; }
        /// <summary>
        /// Identificador de la Caja de referencia cuando es una Nota de crédito Ej: 0
        /// </summary>
        public int Pago_Caj_Ref { get; set; }
        /// <summary>
        /// Identificador de la devolución de referencia cuando es una Nota de crédito Ej: 0
        /// </summary>
        public int Pago_Fac_Ref { get; set; }
        /// <summary>
        /// Identificador del autorizador Ej: 1
        /// </summary>
        public int Autorizador_Id { get; set; }


        public TblVentaFormaPago(int tipo_Id, string pago_Numero, string pago_Aprovacion, int moneda_Id, int banco_Id, string monederoTarj_Id, int tarjeta_Id, double pago_Total, double pago_Total_Moneda, int pago_Total_Tipo_Cambio, int pago_Suc_Ref, int pago_Caj_Ref, int pago_Fac_Ref, int autorizador_Id)
        {
            Tipo_Id = tipo_Id;
            Pago_Numero = pago_Numero;
            Pago_Aprovacion = pago_Aprovacion;
            Moneda_Id = moneda_Id;
            Banco_Id = banco_Id;
            MonederoTarj_Id = monederoTarj_Id;
            Tarjeta_Id = tarjeta_Id;
            Pago_Total = pago_Total;
            Pago_Total_Moneda = pago_Total_Moneda;
            Pago_Total_Tipo_Cambio = pago_Total_Tipo_Cambio;
            Pago_Suc_Ref = pago_Suc_Ref;
            Pago_Caj_Ref = pago_Caj_Ref;
            Pago_Fac_Ref = pago_Fac_Ref;
            Autorizador_Id = autorizador_Id;
        }
    }
}

