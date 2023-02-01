using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasLDCOM.Modelos.Carrito
{
    internal class TotalVenta
    { 
        public string TVenta { get; set; }
        public int TPiezas { get; set; }
        public string TAhorrado { get; set; }

        public string TSubTotal { get; set; }

        public TotalVenta(string Total, int tPiezas, string tAhorrado, string tSubTotal)
        {
            TVenta = Math.Round(decimal.Parse(Total), 2).ToString("N2");
            TPiezas = tPiezas;
            TAhorrado = Math.Round(decimal.Parse(tAhorrado), 2).ToString("N2");
            TSubTotal = Math.Round(decimal.Parse(tSubTotal), 2).ToString("N2");
        }
    }
}
