using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasLDCOM.Modelos.Carrito
{
    class PestanasFormasPago
    {
        public int ID { get; set; }

        [PrimaryKey]
        public string NombreTab { get; set; }
        public string EtiquetaTab { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public Color BGColor_Tab { get; set; }
        public Color BGColor_MarcaTab { get; set; }
        public bool IsVisible { get; set; }
        public string Monto { get; set; }
    }

    //Esta clase solo se debe utilizar para mostrar las Pestañas de las Formas de Pago en Payment
    public class FormasPago_Visual
    {
        public int ID { get; set; }

        [PrimaryKey]
        public string NombreTab { get; set; }
        public string EtiquetaTab { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public Color BGColor_Tab { get; set; }
        public bool IsVisibleTab { get; set; }
        public ImageSource Icono { get; set; }
        public string MontoTab { get; set; }

    }

}

