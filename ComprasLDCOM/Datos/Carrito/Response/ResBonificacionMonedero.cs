using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasLDCOM.Datos.Carrito.Response
{
    public class ResBonificacionMonedero
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }

        public string Data { get; set; }
        public List<BonificacionMonederoDetalle> DataL { get; set; }
    }

    public class BonificacionMonederoDetalle
    {
        public string Articulo { get; set; }
        public int No_Cobradas { get; set; }
        public bool Exitoso { get; set; }
        public int CodigoRespuesta { get; set; }
        public string Articulo_Id { get; set; }
        public string Autorizacion { get; set; }
        public string Operador { get; set; }
        public string Mensaje1 { get; set; }
        public string Mensaje2 { get; set; }
        public string Mensaje3 { get; set; }
        public string ImpresionTicket { get; set; }
        public string NoSecUnico { get; set; }
        public string Transaccion { get; set; }
        public int Visitas { get; set; }
        public string Fecha { get; set; }
        public decimal Saldo { get; set; }
        public decimal MontoOriginalDevolucion { get; set; }
        public decimal Acumula { get; set; }
        public decimal Premio { get; set; }
        public decimal Devolucion { get; set; }
        public string Cliente { get; set; }
        public string FechaRespuesta { get; set; }
        public decimal CargoXServicio { get; set; }
        public int Tipo { get; set; }
        public bool Registrada { get; set; }
        public bool Activa { get; set; }
        public string Laboratorio { get; set; }
        public string LstCupon { get; set; }
        public string HoraServicio { get; set; }
        public string ListaPromocionEspecialAcumulado { get; set; }



        public BonificacionMonederoDetalle(string articulo, int no_Cobradas, bool exitoso, int codigoRespuesta, string articulo_Id, string autorizacion, string operador, string mensaje1, string mensaje2, string mensaje3, string impresionTicket, string noSecUnico, string transaccion, int visitas, string fecha, decimal saldo, decimal montoOriginalDevolucion, decimal acumula, decimal premio, decimal devolucion, string cliente, string fechaRespuesta, decimal cargoXServicio, int tipo, bool registrada, bool activa, string laboratorio, string lstCupon, string horaServicio, string listaPromocionEspecialAcumulado)
        {
            Articulo = articulo;
            No_Cobradas = no_Cobradas;
            Exitoso = exitoso;
            CodigoRespuesta = codigoRespuesta;
            Articulo_Id = articulo_Id;
            Autorizacion = autorizacion;
            Operador = operador;
            Mensaje1 = mensaje1;
            Mensaje2 = mensaje2;
            Mensaje3 = mensaje3;
            ImpresionTicket = impresionTicket;
            NoSecUnico = noSecUnico;
            Transaccion = transaccion;
            Visitas = visitas;
            Fecha = fecha;
            Saldo = saldo;
            MontoOriginalDevolucion = montoOriginalDevolucion;
            Acumula = acumula;
            Premio = premio;
            Devolucion = devolucion;
            Cliente = cliente;
            FechaRespuesta = fechaRespuesta;
            CargoXServicio = cargoXServicio;
            Tipo = tipo;
            Registrada = registrada;
            Activa = activa;
            Laboratorio = laboratorio;
            LstCupon = lstCupon;
            HoraServicio = horaServicio;
            ListaPromocionEspecialAcumulado = listaPromocionEspecialAcumulado;
        }
    }
}
