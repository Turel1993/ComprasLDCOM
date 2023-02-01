using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasLDCOM.Datos.Inicio.Response
{ 
    public class ResArticulo
    {
        public string IsSuccessful { get; set; }
        public string Message { get; set; }

        public List<Arts> Data = new List<Arts>();

        public ResArticulo(string issuccessful, string message, List<Arts> data)
        {
            IsSuccessful = issuccessful;
            Message = message;
            Data = data;
        }
    }

    public class Arts
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public decimal Precio_Maximo { get; set; }
        public decimal Precio_Unitario { get; set; }
        public decimal Desc_Monto { get; set; }
        public string Desc_Nombre { get; set; }
        public decimal Desc_Porc { get; set; }
        public decimal IVA_Porc { get; set; }
        public decimal IVA_Monto { get; set; }
        public decimal IEPS_Porc { get; set; }
        public decimal IEPS_Monto { get; set; }
        public int Cantidad { get; set; }
        public string Promocion { get; set; }
        public string Imagen { get; set; }
        public List<Caract> Caracteristicas { get; set; }
        public bool Tiene_Serie { get; set; }
        public bool Tiene_Caducidad { get; set; }
        public string Articulo_Suelto { get; set; }
        public decimal Costo_Unitario { get; set; }
        public string Articulo_Regalo { get; set; }
        public decimal Promocion_Base { get; set; }
        public decimal Promocion_Regalo { get; set; }
        public bool Controlado { get; set; }
    }

    public class Caract
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }
    }
}
