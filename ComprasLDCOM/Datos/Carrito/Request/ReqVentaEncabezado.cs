using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasLDCOM.Datos.Carrito.Request
{
    public class ReqVentaEncabezado
    {
        /// <summary>
        /// Identificador de la empresa Ej: 1
        /// </summary>
        public int Emp_Id { get; set; }
        /// <summary>
        /// Tipo de Documento según Enum TipoDoc Ej: 1
        /// </summary>
        public int TipoDoc_id { get; set; }
        /// <summary>
        /// Identificador de la Sucursal Ej: 108
        /// </summary>
        public int Suc_Id { get; set; }
        /// <summary>
        /// Identificador de la Caja Ej: 2
        /// </summary>
        public int Caja_Id { get; set; }
        /// <summary>
        /// Tipo de Documento Ej: 1
        /// </summary>
        public int Tipo_Id { get; set; }
        /// <summary>
        /// Identificador del Subtipo de Documento Ej: 1
        /// </summary>
        public int SubDoc_id { get; set; }
        /// <summary>
        /// Identificador del Cliente Ej: 10000022
        /// </summary>
        public long Cliente_Id { get; set; }
        /// <summary>
        /// Identificador del afiliado Ej. -1
        /// </summary>
        public int Afiliado_Id { get; set; }
        /// <summary>
        /// Identificador del Cajero o Usuario Ej: 73
        /// </summary>
        public int Usuario_Id { get; set; }
        /// <summary>
        /// Identificador del Vendedor Ej: 295
        /// </summary>
        public long Vendedor_Id { get; set; }
        /// <summary>
        /// Identificador del Convenio Ej: 0
        /// </summary>
        public int Convenio_Id { get; set; }
        /// <summary>
        /// Numero de Tarjeta de Laboratorio Ej: ""
        /// </summary>
        public string Laboratorio_Tarjeta { get; set; }
        /// <summary>
        /// Identificador de la tarjeta Ej: 0
        /// </summary>
        public int Laboratorio_Id { get; set; }
        /// <summary>
        /// Monto del Cambio que se da en la venta Ej: 0
        /// </summary>
        public double Cambio { get; set; }
        /// <summary>
        /// Numero de cedula del medico Ej: ""
        /// </summary>
        public string Medico_Cedula { get; set; }
        /// <summary>
        /// Número del Monedero o Tarjeta Lealtas
        /// </summary>
        public string MonederoTarj_id { get; set; }
        /// <summary>
        /// Número de la Autorización al hacer la bonificación
        /// </summary>
        public string Monedero_Autorizacion { get; set; }

        public ReqVentaEncabezado(int emp_Id, int tipoDoc_id, int suc_Id, int caja_Id, int tipo_Id, int subDoc_id, long cliente_Id, int afiliado_Id, int usuario_Id, long vendedor_Id, int convenio_Id, string laboratorio_Tarjeta, int laboratorio_Id, double cambio, string medico_Cedula, string monederoTarj_id, string monedero_Autorizacion)
        {
            Emp_Id = emp_Id;
            TipoDoc_id = tipoDoc_id;
            Suc_Id = suc_Id;
            Caja_Id = caja_Id;
            Tipo_Id = tipo_Id;
            SubDoc_id = subDoc_id;
            Cliente_Id = cliente_Id;
            Afiliado_Id = afiliado_Id;
            Usuario_Id = usuario_Id;
            Vendedor_Id = vendedor_Id;
            Convenio_Id = convenio_Id;
            Laboratorio_Tarjeta = laboratorio_Tarjeta;
            Laboratorio_Id = laboratorio_Id;
            Cambio = cambio;
            Medico_Cedula = medico_Cedula;
            MonederoTarj_id = monederoTarj_id;
            Monedero_Autorizacion = monedero_Autorizacion;
        }
    }
}
