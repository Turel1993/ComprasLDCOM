using Microsoft.Maui.Controls.PlatformConfiguration.macOSSpecific;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComprasLDCOM.Popups;
using ComprasLDCOM.Popups.General;
using System.Windows.Input;
using System.ComponentModel;
using ComprasLDCOM.Datos.Carrito.BaseDeDatos;

namespace ComprasLDCOM.Modelos.Carrito
{
    [QueryProperty(nameof(Devolucion), "devolucion")]
    [QueryProperty(nameof(Descuento), "descuento")]
    [QueryProperty(nameof(VentaTotal), "ventaTotal")]
    [QueryProperty(nameof(Cambio), "cambio")]

    internal class TotalesViewModel : BindableObject
    {
        public ObservableCollection<TblPago> pagosList { get; set; } = new();

        public event PropertyChangedEventHandler PropertyChanged;

        #region Variables_Propiedades

        PopupPageEspera popEspera = null;
        public ICommand btnTerminar { get; }

        string totalMontoCapturado = "0";

        private bool EntreCalculo = false;
        private bool EntreLoad = false;
                
        public bool Devolucion
        {
            get => (bool)GetValue(DevolucionProperty);
            set
            {
                SetValue(DevolucionProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Devolucion)));
            }
        }

        public string Descuento
        {
            get => (string)GetValue(DescuentoProperty);
            set
            {
                SetValue(DescuentoProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Descuento)));
            }
        }

        public string VentaTotal
        {
            get => (string)GetValue(VentaTotalProperty);
            set
            {
                SetValue(VentaTotalProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VentaTotal)));
            }
        }

        public string Cambio
        {
            get => (string)GetValue(CambioProperty);
            set
            {
                SetValue(CambioProperty, Convert.ToDecimal(value.Replace(",", "")).ToString("N2"));
                if (EntreLoad)
                    return;
                TotalCompra();
                EntreCalculo = false;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Cambio)));
            }
        }

        public string Subtotal
        {
            get => (string)GetValue(SubtotalProperty);
            set
            {
                SetValue(SubtotalProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Subtotal)));
            }
        }

        public string Total
        {
            get => (string)GetValue(TotalProperty);
            set
            {
                SetValue(TotalProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Total)));
            }
        }
                
        public int TamanoTitulo
        {
            get => (int)GetValue(TamanoTituloProperty);
            set
            {
                SetValue(TamanoTituloProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TamanoTitulo)));
            }
        }

        public int TamanoMonto
        {
            get => (int)GetValue(TamanoMontoProperty);
            set
            {
                SetValue(TamanoMontoProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TamanoMonto)));
            }
        }

        public int TamanoDetalle
        {
            get => (int)GetValue(TamanoDetalleProperty);
            set
            {
                SetValue(TamanoDetalleProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TamanoDetalle)));
            }
        }

        public int TamanoDetalleCollection
        {
            get => (int)GetValue(TamanoDetalleCollectionProperty);
            set
            {
                SetValue(TamanoDetalleCollectionProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TamanoDetalleCollection)));
            }
        }

        public int TamanoRowDetalleCollection
        {
            get => (int)GetValue(TamanoRowDetalleCollectionProperty);
            set
            {
                SetValue(TamanoRowDetalleCollectionProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TamanoRowDetalleCollection)));
            }
        }

        public string TextoOverlay
        {
            get => (string)GetValue(TextoOverlayProperty);
            set
            {
                SetValue(TextoOverlayProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextoOverlay)));
            }
        }

        public string TituloTotal
        {
            get => (string)GetValue(TituloTotalProperty);
            set
            {
                SetValue(TituloTotalProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TituloTotal)));
            }
        }

        public string TituloPagoCon
        {
            get => (string)GetValue(TituloPagoConProperty);
            set
            {
                SetValue(TituloPagoConProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TituloPagoCon)));
            }
        }

        #endregion

        #region BindableProperty
        public BindableProperty DevolucionProperty = BindableProperty.Create(nameof(Devolucion), typeof(bool), typeof(TotalesViewModel));
        public BindableProperty DescuentoProperty = BindableProperty.Create(nameof(Descuento), typeof(string), typeof(TotalesViewModel));
        public BindableProperty VentaTotalProperty = BindableProperty.Create(nameof(VentaTotal), typeof(string), typeof(TotalesViewModel));
        public BindableProperty CambioProperty = BindableProperty.Create(nameof(Cambio), typeof(string), typeof(TotalesViewModel));
        public BindableProperty SubtotalProperty = BindableProperty.Create(nameof(Subtotal), typeof(string), typeof(TotalesViewModel));
        public BindableProperty TotalProperty = BindableProperty.Create(nameof(Total), typeof(string), typeof(TotalesViewModel));
        public BindableProperty TamanoTituloProperty = BindableProperty.Create(nameof(TamanoTitulo), typeof(int), typeof(TotalesViewModel));
        public BindableProperty TamanoMontoProperty = BindableProperty.Create(nameof(TamanoMonto), typeof(int), typeof(TotalesViewModel));
        public BindableProperty TamanoDetalleProperty = BindableProperty.Create(nameof(TamanoDetalle), typeof(int), typeof(TotalesViewModel));
        public BindableProperty TamanoDetalleCollectionProperty = BindableProperty.Create(nameof(TamanoDetalleCollection), typeof(int), typeof(TotalesViewModel));
        public BindableProperty TamanoRowDetalleCollectionProperty = BindableProperty.Create(nameof(TamanoRowDetalleCollection), typeof(int), typeof(TotalesViewModel));
        public BindableProperty TextoOverlayProperty = BindableProperty.Create(nameof(TextoOverlay), typeof(string), typeof(TotalesViewModel));
        public BindableProperty TituloTotalProperty = BindableProperty.Create(nameof(TituloTotal), typeof(string), typeof(TotalesViewModel));
        public BindableProperty TituloPagoConProperty = BindableProperty.Create(nameof(TituloPagoCon), typeof(string), typeof(TotalesViewModel));

        #endregion    

        /// <summary>
        /// Cierra el PopUp de Espera
        /// </summary>
        private void PopupCerrar()
        {
            try
            {
                if (popEspera != null)
                {
                    popEspera.CerrarPopup();
                    popEspera = null;
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        public TotalesViewModel()
        {
            if (App.Funciones.EsDispositivoPequeño())
            {
                TamanoTitulo = 18;
                TamanoMonto = 16;
                TamanoDetalle = 18;
                TamanoDetalleCollection = 200;
                TamanoRowDetalleCollection = 20;
            }
            else
            {
                TamanoTitulo = 30;
                TamanoMonto = 30;
                TamanoDetalle = 22;
                TamanoDetalleCollection = 240;
                TamanoRowDetalleCollection = 21;
            }

            EntreLoad = true;
            Subtotal = "0.00";
            VentaTotal = "0.00";
            Descuento = "0.00";
            Total = "0.00";
            Cambio = "0.00";
            get_Pagos();
            btnTerminar = new Command(btn_Terminar);
            popEspera = null;
            EntreLoad = false;
        }

        /// <summary>
        /// Obtiene el detalle de las Formas de Pago
        /// </summary>
        public void get_Pagos()
        {
            try
            {
                TblPago P;
                pagosList.Clear();
                List<TblPayment> FormasPago = App.ServiciosBD.ObtenerListaEntidadLocal(new TblPayment().GetType().Name).OfType<TblPayment>().ToList();

                if (FormasPago.Where(x => x.TipoPago.ToUpper() == "EFECTIVO").ToList().Count > 0)
                    P = new TblPago(1, "EFECTIVO", float.Parse(FormasPago.Where(x => x.TipoPago.ToUpper() == "EFECTIVO").ToList()[0].MontoCapturado), (App.Funciones.EsDispositivoPequeño() ? 17 : 19), (App.Funciones.EsDispositivoPequeño() ? 18 : 26));
                else
                    P = new TblPago(1, "EFECTIVO", 0, (App.Funciones.EsDispositivoPequeño() ? 17 : 19), (App.Funciones.EsDispositivoPequeño() ? 18 : 26));
                pagosList.Add(P);

                if (FormasPago.Where(x => x.TipoPago.ToUpper() == "TARJETA BANCARIA").ToList().Count > 0)
                    P = new TblPago(1, "TARJETA BANCARIA", float.Parse(FormasPago.Where(x => x.TipoPago.ToUpper() == "TARJETA BANCARIA").ToList()[0].MontoCapturado), (App.Funciones.EsDispositivoPequeño() ? 17 : 19), (App.Funciones.EsDispositivoPequeño() ? 18 : 26));
                else
                    P = new TblPago(1, "TARJETA BANCARIA", 0, (App.Funciones.EsDispositivoPequeño() ? 17 : 19), (App.Funciones.EsDispositivoPequeño() ? 18 : 26));
                pagosList.Add(P);                

                if (FormasPago.Where(x => x.TipoPago.ToUpper() == "MONEDERO").ToList().Count > 0)
                    P = new TblPago(1, "MONEDERO", float.Parse(FormasPago.Where(x => x.TipoPago.ToUpper() == "MONEDERO").ToList()[0].MontoCapturado), (App.Funciones.EsDispositivoPequeño() ? 17 : 19), (App.Funciones.EsDispositivoPequeño() ? 18 : 26));
                else
                    P = new TblPago(1, "MONEDERO", 0, (App.Funciones.EsDispositivoPequeño() ? 17 : 19), (App.Funciones.EsDispositivoPequeño() ? 18 : 26));
                pagosList.Add(P);                                

                totalMontoCapturado = FormasPago.Sum(x => Convert.ToDecimal(x.MontoCapturado)).ToString("N2");
            }
            catch (Exception e)
            {
                System.Console.Write(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el Total del Monto a pagar 
        /// </summary>
        public void TotalCompra()
        {
            if (EntreCalculo)
                return;

            EntreCalculo = true;
            if (!Devolucion)
            {
                TituloTotal = "VENTA TOTAL";
                TituloPagoCon = "PAGO CON:";
                List<TblCarrito> lista = App.ServiciosBD.ObtenerListaEntidadLocal(new TblCarrito().GetType().Name).OfType<TblCarrito>().ToList();
                Subtotal = VentaTotal;
            }
            else
            {
                TituloTotal = "DEVOLUCION TOTAL";
                TituloPagoCon = "SE DEVOLVIO A:";
                Subtotal = pagosList.Sum(x => Convert.ToDecimal(x.Monto)).ToString("N2");
            }

            if (App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblGeneralesVenta", App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblConfiguracion", "CLIENTE_VIRTUAL", "Valor"), "IdTipoDocumento") == "4" || Devolucion)
                Cambio = "0.00";

            Total = decimal.Parse(Subtotal).ToString("N2");
        }

        /// <summary>
        /// Botón para Terminar la venta
        /// </summary>
        public async void btn_Terminar()
        {
            TextoOverlay = "Terminando venta ...";
            App.Funciones.PopupEspera(); 
            BorrarDatos_Venta();
            await Shell.Current.GoToAsync("Iniciopage");
            PopupCerrar();
        }

        /// <summary>
        /// Borra los datos de la Compra (Artículos y Formas de Pago)
        /// </summary>
        public void BorrarDatos_Venta()
        {
            App.ServiciosBD.LimpiarEntidadLocal(new TblCarrito());
            App.ServiciosBD.LimpiarEntidadLocal(new TblPayment());
            App.ServiciosBD.LimpiarEntidadLocal(new TblGeneralesVenta());
            App.ServiciosBD.LimpiarEntidadLocal(new TblArticulosCupones());
        }
        
    }
}

