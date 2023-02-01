using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using ComprasLDCOM.API;
using ComprasLDCOM.Popups.General;
using ComprasLDCOM.Datos.Carrito.BaseDeDatos;
using ComprasLDCOM.Modelos.Carrito;
using ComprasLDCOM.Popups.Carrito;

namespace ComprasLDCOM.Modelos.Carrito
{
    [QueryProperty(nameof(SubTotal), "subtotal")]
    [QueryProperty(nameof(IVA), "iva")]
    [QueryProperty(nameof(IEPS), "ieps")]
    [QueryProperty(nameof(Promociones), "promociones")]
    [QueryProperty(nameof(Descuentos), "descuentos")]
    [QueryProperty(nameof(Total), "total")]
    internal class PaymentViewModel : BindableObject, INotifyPropertyChanged
    {
        ApiDataStore DStore = new();        

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #region ObservableCollection
        private ObservableCollection<Datos.Carrito.Response.ConsultaSocioFrecuenteDetalle> _monederoList { get; set; } = new();
        private ObservableCollection<FormasPago_Visual> _formaPagoList { get; set; } = new();

        public ObservableCollection<FormasPago_Visual> Pestana_FormaPagoList
        {
            get { return _formaPagoList; }
            set
            {
                _formaPagoList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Pestana_FormaPagoList)));
            }
        }

        public ObservableCollection<Datos.Carrito.Response.ConsultaSocioFrecuenteDetalle> monederoList
        {
            get { return _monederoList; }
            set
            {
                _monederoList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(monederoList)));
            }
        }

        #endregion


        #region Variables_Propiedades  

        PopupPageEspera popEspera;
        PopupPageEfectivo popEfectivo;
        PopupPageTarjetaBancaria popTarjetaBancaria;
        PopupPageMonedero popMonedero;
        /// <summary>
        /// Variable para el Popup de Busqueda Monedero
        /// </summary>
        PopupPageBusquedaMonedero popBusquedaMonedero = null;

        bool NuevoMonedero = false;
        bool RedimiendoMonedero = false;

        /// <summary>
        /// Saldo que tiene el monedero seleccionado
        /// </summary>
        private decimal SaldoMonedero = 0;
        
        /// <summary>
        /// Id del Socio del monedero seleccionado
        /// </summary>
        private int IdSocio_Monedero = 0;

        /// <summary>
        /// Color del cuadro Total a Pagar
        /// </summary>
        private Color color_TotalPagar { set; get; }

        /// <summary>
        /// Color del Switch de la Tarjeta Bancaria
        /// </summary>
        private Color color_SwitchTB { set; get; }

        /// <summary>
        /// Color del Tab de Efectivo
        /// </summary>
        private Color color_TabEfectivo { get; set; }

        /// <summary>
        /// Color del Tab de Tarjeta Bancaria
        /// </summary>
        private Color color_TabTarjeta { get; set; }

        /// <summary>
        /// Color del Tab de Nota de Credito
        /// </summary>
        private Color color_TabNotaCredito { get; set; }

        /// <summary>
        /// Color del Tab de Cupones
        /// </summary>
        private Color color_TabCupones { get; set; }

        /// <summary>
        /// Color del Tab de Monedero
        /// </summary>
        private Color color_TabMonedero { get; set; }

        /// <summary>
        /// Color del rombo en el Tab de Efectivo
        /// </summary>
        private Color color_MarcaTabEfectivo { get; set; }

        /// <summary>
        /// Color del rombo en el Tab de Tarjeta Bancaria
        /// </summary>
        private Color color_MarcaTabTarjeta { get; set; }

        /// <summary>
        /// Color del rombo del Tab de Nota de Credito
        /// </summary>
        private Color color_MarcaTabNotaCredito { get; set; }

        /// <summary>
        /// Color del rombo del Tab de Cupones
        /// </summary>
        private Color color_MarcaTabCupones { get; set; }

        /// <summary>
        /// Color del rombo del Tab de Monedero
        /// </summary>
        private Color color_MarcaTabMonedero { get; set; }

        /// <summary>
        /// Propiedad para el color del Tab de Efectivo
        /// </summary>
        public Color BGColor_TabEfectivo
        {
            set
            {
                if (color_TabEfectivo != value)
                {
                    color_TabEfectivo = value;
                    OnPropertyChanged("BGColor_TabEfectivo");
                }
            }

            get { return color_TabEfectivo; }
        }

        /// <summary>
        /// Propiedad para el color del Tab de Tarjeta Bancaria
        /// </summary>
        public Color BGColor_TabTarjeta
        {
            set
            {
                if (color_TabTarjeta != value)
                {
                    color_TabTarjeta = value;
                    OnPropertyChanged("BGColor_TabTarjeta");
                }
            }

            get { return color_TabTarjeta; }
        }

        /// <summary>
        /// Propiedad para el color del tab de Monedero
        /// </summary>
        public Color BGColor_TabMonedero
        {
            set
            {
                if (color_TabMonedero != value)
                {
                    color_TabMonedero = value;
                    OnPropertyChanged("BGColor_TabMonedero");
                }
            }

            get { return color_TabMonedero; }
        }        

        /// <summary>
        /// Propiedad para el color del cuadro de Total a Pagar
        /// </summary>
        public Color BGColor_TotalPagar
        {
            set
            {
                if (color_TotalPagar != value)
                {
                    color_TotalPagar = value;
                    OnPropertyChanged("BGColor_TotalPagar");
                }
            }

            get { return color_TotalPagar; }
        }        

        /// <summary>
        /// Muestra el Popup de Efectivo
        /// </summary>
        private void PopupEfectivo()
        {
            popEfectivo = new();
            Application.Current.MainPage.ShowPopup(popEfectivo);
        }

        /// <summary>
        /// Muestra el Popup de Tarjeta Bancaria
        /// </summary>
        private void PopupTarjetaBancaria()
        {
            popTarjetaBancaria = new();
            Application.Current.MainPage.ShowPopup(popTarjetaBancaria);
        }

        /// <summary>
        /// Muestra el Popup de Monedero
        /// </summary>
        private void PopupMonedero()
        {
            popMonedero = new();
            Application.Current.MainPage.ShowPopup(popMonedero);
        }

        /// <summary>
        /// Boton de una cantidad o denominacion de Efectivo $20, $50, etc.
        /// </summary>
        public ICommand EfectivoCantidad { get; }

        /// <summary>
        /// Boton de un digito de Efectivo 1, 2, 3, etc.
        /// </summary>
        public ICommand EfectivoDigito { get; }

        /// <summary>
        /// Boton para borrar un digito de la cantidad de Efectivo
        /// </summary>
        public ICommand EfectivoBorrarDigito { get; }

        /// <summary>
        /// Boton de Cobrar 
        /// </summary>
        public ICommand btnCobrar { get; }

        /// <summary>
        /// Boton de Cancelar 
        /// </summary>
        public ICommand btnCancelar { get; }

        /// <summary>
        /// Agrega un pago en la lista de pagos
        /// </summary>
        public ICommand AgregarPago { get; }

        /// <summary>
        /// Tab seleccionado
        /// </summary>
        public ICommand TabActivo { get; }
                
        /// <summary>
        /// Botón para cerrar el PopupEfectivo
        /// </summary>
        public ICommand btnAceptarPopupEfectivo { get; }

        /// <summary>
        /// Botón para cerrar el PopupTarjetaBancaria
        /// </summary>
        public ICommand btnAceptarPopupTarjetaBancaria { get; }
                
        /// <summary>
        /// Boton para Aceptar el PopupMonedero
        /// </summary>
        public ICommand btnAceptarPopupMonedero { get; }
        
        /// <summary>
        /// Botón para buscar un Monedero_Lealtad
        /// </summary>
        public ICommand btnBuscarMonederoPopUp { get; }

        /// <summary>
        /// Botón OK del registro del Monedero encontrado en la Ventana Popup para seleccionar el Monedero
        /// </summary>
        public ICommand btnMonederoOK { get; }

        /// <summary>
        /// Botón para cerrar el Popup de Búsqueda Monedero
        /// </summary>
        public ICommand PopupBusquedaMonederoCerrar { get; }

        /// <summary>
        /// Monto Total pagado en Efectivo
        /// </summary>        
        string cantidadEfectivoPagado = "0.00";

        /// <summary>
        /// Monto capturado en Efectivo
        /// </summary>
        string cantidadEfectivo = "0.00";

        /// <summary>
        /// Monto capturado con Tarjeta Bancaria
        /// </summary>
        string cantidadTarjetaBancaria = "0.00";

        /// <summary>
        /// Monto capturado con Cupones o Descuentos
        /// </summary>
        string cantidadMonedero = "0.00";

        /// <summary>
        /// Monto Total capturado en todas las Formas de Pago
        /// </summary>
        string cantidadMontoCapturado = "0.00";

        /// <summary>
        /// Monto Total Restante de la Veta
        /// </summary>
        string cantidadMontoRestante = "0.00";

        /// <summary>
        /// Monto en Efectivo capturado con los dígitos del 0 al 9
        /// </summary>
        string digitos = "";
       
        /// <summary>
        /// Número de autorización que regresa la Api al hacer el Pago con Monedero
        /// </summary>
        private string PagoMonedero_Autorizacion = "";

        /// <summary>
        /// Número de autorización que regresa la Api al hacer la Bonificación al Monedero
        /// </summary>
        private string BonificacionMonedero_Autorizacion = "";

        /// <summary>
        /// Total del Monto a pagar antes de aplicar descuentos y promociones
        /// </summary>
        private string _subtotal = "0";
        /// <summary>
        /// Propiedad para el Total del Monto a pagar antes de aplicar descuentos y promociones
        /// </summary>
        public string SubTotal
        {
            get => _subtotal;
            set
            {
                if (_subtotal != value)
                {
                    _subtotal = Convert.ToDecimal(value).ToString("N2");
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// IVA que se recibe de la página del Carrito
        /// </summary>
        private string _iva = "0";
        public string IVA
        {
            get => _iva;
            set
            {
                if (_iva != value)
                {
                    _iva = Convert.ToDecimal(value).ToString("N2");
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// IEPS que se recibe de la página del Carrito
        /// </summary>
        private string _ieps = "0";
        public string IEPS
        {
            get => _ieps;
            set
            {
                if (_ieps != value)
                {
                    _ieps = Convert.ToDecimal(value).ToString("N2");
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Total del Monto de las promociones que tiene el carrito
        /// </summary>
        private string _promociones = "0";
        
        /// <summary>
        /// Propiedad para el Total del Monto de las promociones que tiene el carrito
        /// </summary>
        public string Promociones
        {
            get => _promociones;
            set
            {
                if (_promociones != value)
                {
                    _promociones = Convert.ToDecimal(value).ToString("N2");
                    OnPropertyChanged();
                }
            }
        }
       
        /// <summary>
        /// Total del Monto de los descuentos que tiene el carrito
        /// </summary>
        private string _totalDescuentos = "0";
        
        /// <summary>
        /// Propiedad para el Total del Monto de los descuentos que tiene el carrito
        /// </summary>
        public string Descuentos
        {
            get => _totalDescuentos;
            set
            {
                if (_totalDescuentos != value)
                {
                    _totalDescuentos = Convert.ToDecimal(value).ToString("N2");
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Total del Monto final a pagar ya aplicados los descuentos y promociones 
        /// </summary>
        private string _total = "0";
        /// <summary>
        /// Propiedad para el Total del Monto final a pagar ya aplicados los descuentos y promociones
        /// </summary>
        public string Total
        {
            get => _total;
            set
            {
                if (_total != value)
                {
                    _total = Convert.ToDecimal(value).ToString("N2");
                    Actualizar_MontoTotalCapturado();
                    Total_EfectivoPagado = ((float.Parse(SubTotal) - float.Parse(Descuentos) - float.Parse(Promociones) - float.Parse(Total_Monedero))).ToString();

                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Monto de la Tarjeta Bancaria capturado
        /// </summary>
        private string montoPagoTB;

        /// <summary>
        /// Propiedad para el Monto de la Tarjeta Bancaria capturado
        /// </summary>
        public string MontoPagoTB
        {
            get => montoPagoTB;
            set
            {
                if (montoPagoTB == value)
                    return;
                montoPagoTB = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Número de Tarjeta Bancaria capturado
        /// </summary>
        private string numTarjeta;

        /// <summary>
        /// Propiedad para el Numero de Tarjeta Bancaria capturado
        /// </summary>
        public string NumTarjeta
        {
            get => numTarjeta;
            set
            {
                if (numTarjeta == value)
                    return;
                numTarjeta = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Nombre del Tarjetahabiente capturado
        /// </summary>
        private string nombreTarjetahabiente;

        /// <summary>
        /// Propiedad para el Nombre del Tarjetahabiente capturado
        /// </summary>
        public string NombreTarjetahabiente
        {
            get => nombreTarjetahabiente;
            set
            {

                if (nombreTarjetahabiente == value)
                    return;
                nombreTarjetahabiente = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Número de Monedero capturado
        /// </summary>
        private string numMonedero;

        /// <summary>
        /// Propiedad para el Numero de Cupón capturado
        /// </summary>
        public string NumMonedero
        {
            get => numMonedero;
            set
            {
                if (numMonedero == value)
                    return;
                numMonedero = value;
                OnPropertyChanged();
            }
        }
        
        /// <summary>
        /// Propiedada para el Monto pagado en Efectivo
        /// </summary>
        public string Total_EfectivoPagado
        {
            get { return cantidadEfectivoPagado; }

            set
            {
                if (value == cantidadEfectivoPagado)
                    return;

                cantidadEfectivoPagado = Math.Round(float.Parse(value.ToString()), 2).ToString("N2");

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Propiedad para el Monto Total capturado en Efectivo $20, $50, $100, etc.
        /// </summary>
        public string Total_Efectivo
        {
            get { return cantidadEfectivo; }

            set
            {
                if (value == cantidadEfectivo)
                    return;

                cantidadEfectivo = Math.Round(float.Parse(value.ToString()), 2).ToString("N2");
                if (float.Parse(cantidadEfectivo) > 0)
                {
                    OnPropertyChanged();
                }
                else
                Total_EfectivoPagado = ((float.Parse(SubTotal) - float.Parse(Descuentos) - float.Parse(Promociones))).ToString();


                //Se modifica el color de la pestaña, se borra la lista de las pestañas y se vuelve a cargar para mostrar el cambio de color en la vista
                ObservableCollection<FormasPago_Visual> nuevaLista = new();
                foreach (var pago in Pestana_FormaPagoList)
                {
                    if (pago.EtiquetaTab == "Efectivo")
                    {
                        nuevaLista.Add(new FormasPago_Visual { ID = pago.ID, NombreTab = pago.NombreTab, EtiquetaTab = pago.EtiquetaTab, Nombre1 = pago.Nombre1, Nombre2 = pago.Nombre2, BGColor_Tab = pago.BGColor_Tab, Icono = pago.Icono, IsVisibleTab = pago.IsVisibleTab, MontoTab = cantidadEfectivo });
                    }
                    else
                    {
                        nuevaLista.Add(new FormasPago_Visual { ID = pago.ID, NombreTab = pago.NombreTab, EtiquetaTab = pago.EtiquetaTab, Nombre1 = pago.Nombre1, Nombre2 = pago.Nombre2, BGColor_Tab = pago.BGColor_Tab, Icono = pago.Icono, IsVisibleTab = pago.IsVisibleTab, MontoTab = pago.MontoTab != null ? pago.MontoTab : "0.00" });
                    }
                }

                Pestana_FormaPagoList.Clear();
                Pestana_FormaPagoList = nuevaLista;


                OnPropertyChanged();

            }
        }

        /// <summary>
        /// Propiedad para el Monto Total capturado en Tarjeta Bancaria
        /// </summary>
        public string Total_TarjetaBancaria
        {
            get { return cantidadTarjetaBancaria; }

            set
            {
                if (value == cantidadTarjetaBancaria)
                    return;

                cantidadTarjetaBancaria = Math.Round(float.Parse(value.ToString()), 2).ToString("N2");
                
                //Se modifica el color de la pestaña, se borra la lista de las pestañas y se vuelve a cargar para mostrar el cambio de color en la vista
                ObservableCollection<FormasPago_Visual> nuevaLista = new();
                foreach (var pago in Pestana_FormaPagoList)
                {
                    if (pago.EtiquetaTab == "Tarjeta Bancaria")
                    {
                        nuevaLista.Add(new FormasPago_Visual { ID = pago.ID, NombreTab = pago.NombreTab, EtiquetaTab = pago.EtiquetaTab, Nombre1 = pago.Nombre1, Nombre2 = pago.Nombre2, BGColor_Tab = pago.BGColor_Tab, Icono = pago.Icono, IsVisibleTab = pago.IsVisibleTab, MontoTab = cantidadTarjetaBancaria });
                    }
                    else
                    {
                        nuevaLista.Add(new FormasPago_Visual { ID = pago.ID, NombreTab = pago.NombreTab, EtiquetaTab = pago.EtiquetaTab, Nombre1 = pago.Nombre1, Nombre2 = pago.Nombre2, BGColor_Tab = pago.BGColor_Tab, Icono = pago.Icono, IsVisibleTab = pago.IsVisibleTab, MontoTab = pago.MontoTab != null ? pago.MontoTab : "0.00" });
                    }
                }

                Pestana_FormaPagoList.Clear();
                Pestana_FormaPagoList = nuevaLista;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Propiedad para el Monto Total caprutado en Monedero
        /// </summary>
        public string Total_Monedero
        {
            get { return cantidadMonedero; }

            set
            {
                ObservableCollection<FormasPago_Visual> nuevaLista = new();
                if (value == cantidadMonedero)
                {
                    //Se modifica el color de la pestaña, se borra la lista de las pestañas y se vuelve a cargar para mostrar el cambio de color en la vista                    
                    foreach (var pago in Pestana_FormaPagoList)
                    {
                        if (pago.EtiquetaTab == "Efectivo")
                        {
                            nuevaLista.Add(new FormasPago_Visual { ID = pago.ID, NombreTab = pago.NombreTab, EtiquetaTab = pago.EtiquetaTab, Nombre1 = pago.Nombre1, Nombre2 = pago.Nombre2, BGColor_Tab = pago.BGColor_Tab, Icono = pago.Icono, IsVisibleTab = pago.IsVisibleTab, MontoTab = cantidadEfectivo });
                        }
                        else
                        {
                            nuevaLista.Add(new FormasPago_Visual { ID = pago.ID, NombreTab = pago.NombreTab, EtiquetaTab = pago.EtiquetaTab, Nombre1 = pago.Nombre1, Nombre2 = pago.Nombre2, BGColor_Tab = pago.BGColor_Tab, Icono = pago.Icono, IsVisibleTab = pago.IsVisibleTab, MontoTab = pago.MontoTab != null ? pago.MontoTab : "0.00" });
                        }
                    }

                    Pestana_FormaPagoList.Clear();
                    Pestana_FormaPagoList = nuevaLista;
                    return;
                }


                cantidadMonedero = Math.Round(float.Parse(value.ToString()), 2).ToString("N2");
                
                //Se modifica el color de la pestaña, se borra la lista de las pestañas y se vuelve a cargar para mostrar el cambio de color en la vista
                //ObservableCollection<FormasPago_Visual> nuevaLista = new();
                foreach (var pago in Pestana_FormaPagoList)
                {
                    if (pago.EtiquetaTab == "Monedero")
                    {
                        nuevaLista.Add(new FormasPago_Visual { ID = pago.ID, NombreTab = pago.NombreTab, EtiquetaTab = pago.EtiquetaTab, Nombre1 = pago.Nombre1, Nombre2 = pago.Nombre2, BGColor_Tab = pago.BGColor_Tab, Icono = pago.Icono, IsVisibleTab = pago.IsVisibleTab, MontoTab = cantidadMonedero });
                    }
                    else
                    {
                        nuevaLista.Add(new FormasPago_Visual { ID = pago.ID, NombreTab = pago.NombreTab, EtiquetaTab = pago.EtiquetaTab, Nombre1 = pago.Nombre1, Nombre2 = pago.Nombre2, BGColor_Tab = pago.BGColor_Tab, Icono = pago.Icono, IsVisibleTab = pago.IsVisibleTab, MontoTab = pago.MontoTab != null ? pago.MontoTab : "0.00" });
                    }
                }

                Pestana_FormaPagoList.Clear();
                Pestana_FormaPagoList = nuevaLista;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Propiedad para la Suma del Monto Total capturado en todas las forma de pago
        /// </summary>
        public string Total_MontoCapturado
        {
            get { return cantidadMontoCapturado; }

            set
            {
                if (value == cantidadMontoCapturado)
                    return;

                cantidadMontoCapturado = Math.Round(float.Parse(value.ToString()), 2).ToString("N2"); //value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Propiedad para la Suma del Monto Restante para la Venta
        /// </summary>
        public string Total_MontoRestante
        {
            get { return cantidadMontoRestante; }

            set
            {
                if (value == cantidadMontoRestante)
                    return;

                cantidadMontoRestante = Math.Round(float.Parse(value.ToString()), 2).ToString("N2");
                OnPropertyChanged();
            }
        }        

        /// <summary>
        /// Dato General de Venta del Documento
        /// </summary>
        private string dgv_documento;
        
        /// <summary>
        /// Propiedad para el Dato General de Venta Documento
        /// </summary>
        public string DGV_Documento
        {
            get => dgv_documento;
            set
            {
                if (dgv_documento == value)
                    return;
                dgv_documento = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Dato General de Venta del Monedero
        /// </summary>
        private string dgv_monedero;
        
        /// <summary>
        /// Propiedad para el Dato General de Venta Monedero
        /// </summary>
        public string DGV_Monedero
        {
            get => dgv_monedero;
            set
            {
                if (dgv_monedero == value)
                    return;
                dgv_monedero = value;
                OnPropertyChanged();
            }
        }
        
        /// <summary>
        /// Mensaje del Monedero
        /// </summary>
        string mensajeMonedero = "";
       
        /// <summary>
        /// Propiedad para el Mensaje del Monedero
        /// </summary>
        public string MensajeMonedero
        {
            get { return mensajeMonedero; }

            set
            {
                if (value == mensajeMonedero)
                    return;

                mensajeMonedero = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Mensaje a mostrar el el Popup de Espera
        /// </summary>
        public string TextoOverlay
        {
            get => (string)GetValue(TextoOverlayProperty);
            set
            {
                SetValue(TextoOverlayProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextoOverlay)));
            }
        }

        /// <summary>
        /// Propiedad para el Habilitar el Frame de Efectivo
        /// </summary>
        public bool TabEfectivoVisible
        {
            set
            {
                SetValue(TabEfectivoProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TabEfectivoVisible)));
            }

            get => (bool)GetValue(TabEfectivoProperty);

        }

        /// <summary>
        /// Propiedad para el Habilitar el Frame de Tarjeta Bancaria
        /// </summary>
        public bool TabTarjetaVisible
        {
            set
            {
                SetValue(TabTarjetaProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TabTarjetaVisible)));
            }

            get => (bool)GetValue(TabTarjetaProperty);
        }

        /// <summary>
        /// Propiedad para el Habilitar el Frame de Monedero
        /// </summary>
        public bool TabMonederoVisible
        {
            set
            {
                SetValue(TabMonederoProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TabMonederoVisible)));
            }

            get => (bool)GetValue(TabMonederoProperty);
        }
        
        /// <summary>
        /// Variable para saber si se va a Redimir en Monedero_Lealtad
        /// </summary>
        private bool isSelected_Redimir;

        /// <summary>
        /// Propiedad para asignar el valor a Redimir en Monedero_Lealtad
        /// </summary>
        public bool IsSelected_Redimir
        {
            get { return isSelected_Redimir; }
            set
            {
                if (isSelected_Redimir == value)
                    return;
                else
                {
                    isSelected_Redimir = value;
                    OnPropertyChanged();
                }

                if (IsSelected_Redimir == true)
                {
                    RedimiendoMonedero = true;
                    Obtener_MontoMonedero();
                }
                else
                {
                    Total_Monedero = "0.00";
                    MensajeMonedero = "";
                    RedimiendoMonedero = false;
                    Actualizar_MontoTotalCapturado();
                }
            }
        }        

        /// <summary>
        /// Propiedad para el Habilitar el Frame de Tarjeta Bancaria
        /// </summary>
        public bool PopUpVisible
        {
            set
            {
                SetValue(PopUpProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PopUpVisible)));
            }

            get => (bool)GetValue(PopUpProperty);
        }

        /// <summary>
        /// Propiedad para el Número del Monedero_Lealtad capturado en la ventana emergente
        /// </summary>
        public string NumMonederoLealtadPopUp
        {
            set
            {
                SetValue(numMonederoProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NumMonederoLealtadPopUp)));
            }

            get => (string)GetValue(numMonederoProperty);
        }

        /// <summary>
        /// Botón para cambiar o agregar el monedero desde la barra superior en la Vista
        /// </summary>
        public ICommand Cambiarmonedero => new Command(void () =>
        {
            if (!RedimiendoMonedero)
            {
                NumMonederoLealtadPopUp = "";
                NuevoMonedero = true;
                Popup_BusquedaMonedero_Abrir();
            }
            else
                App.Current.MainPage.DisplayAlert("Aviso", "No se puede cambiar el Monedero, cuando en la forma de pago está seleccionado para redimir.", "Aceptar");
        });

        #endregion


        #region BindableProperty
        public BindableProperty TabEfectivoProperty = BindableProperty.Create(nameof(TabEfectivoVisible), typeof(bool), typeof(PaymentViewModel));
        public BindableProperty TabTarjetaProperty = BindableProperty.Create(nameof(TabTarjetaVisible), typeof(bool), typeof(PaymentViewModel));
        public BindableProperty TabMonederoProperty = BindableProperty.Create(nameof(TabMonederoVisible), typeof(bool), typeof(PaymentViewModel));
        public BindableProperty PopUpProperty = BindableProperty.Create(nameof(PopUpVisible), typeof(bool), typeof(PaymentViewModel));
        public BindableProperty TextoOverlayProperty = BindableProperty.Create(nameof(TextoOverlay), typeof(string), typeof(PaymentViewModel));
        public BindableProperty numMonederoProperty = BindableProperty.Create(nameof(NumMonederoLealtadPopUp), typeof(string), typeof(PaymentViewModel));

        #endregion


        /// <summary>
        /// Mostrar PopupEspera
        /// </summary>
        /// <param name="Texto">Texto a Mostrar en Popup</param>
        private void PopupEspera(string Texto)
        {
            popEspera = new();
            TextoOverlay = Texto;
            Application.Current.MainPage.ShowPopup(popEspera);
        }

        /// <summary>
        /// Cerrar Popup
        /// </summary>
        private void PopupCerrar()
        {
            if (popEspera != null)
                popEspera.CerrarPopup();
        }

        /// <summary>
        /// Se muestra el PopUp de Búsqueda Monedero
        /// </summary>
        private void Popup_BusquedaMonedero_Abrir()
        {
            popBusquedaMonedero = new();
            Application.Current.MainPage.ShowPopup(popBusquedaMonedero);
        }

        /// <summary>
        /// Se cierra el PopUp de Búsqueda Monedero
        /// </summary>
        private void Popup_BusquedaMonedero_Cerrar()
        {
            monederoList.Clear();
            popBusquedaMonedero.CerrarPopup();
            popBusquedaMonedero = null;
        }


        public PaymentViewModel()
        {
            PopUpVisible = true;

            //Se crean las pestañas con sus datos correspondientes
            Pestana_FormaPagoList.Add(new FormasPago_Visual { ID = 1, NombreTab = "TabEfectivo", EtiquetaTab = "Efectivo", Nombre1 = "Efectivo", Nombre2 = "", BGColor_Tab = Color.FromRgb(16, 108, 7), Icono = "money_launcher_adaptive_fore.png", IsVisibleTab = true, MontoTab = "0.00" });
            Pestana_FormaPagoList.Add(new FormasPago_Visual { ID = 2, NombreTab = "TabTarjetaBancaria", EtiquetaTab = "Tarjeta Bancaria", Nombre1 = "Tarjeta", Nombre2 = "Bancaria", BGColor_Tab = Color.FromRgb(52, 73, 94), Icono = "card_launcher_adaptive_fore.png", IsVisibleTab = true, MontoTab = "0.00" });
            Pestana_FormaPagoList.Add(new FormasPago_Visual { ID = 5, NombreTab = "TabMonedero", EtiquetaTab = "Monedero", Nombre1 = "Monedero", Nombre2 = "", BGColor_Tab = Color.FromRgb(52, 73, 94), Icono = "card_launcher_adaptive_fore.png", IsVisibleTab = true, MontoTab = "0.00" });

            EfectivoCantidad = new Command(Efectivo_Cantidad);
            EfectivoDigito = new Command(Efectivo_Digito);
            EfectivoBorrarDigito = new Command(Efectivo_BorrarDigito);
            btnCobrar = new Command(btn_Cobrar);
            btnCancelar = new Command(btn_Cancelar);
            btnAceptarPopupEfectivo = new Command(btn_AceptarPopupEfectivo);
            btnAceptarPopupTarjetaBancaria = new Command(btn_AceptarPopupTarjetaBancaria);
            btnAceptarPopupMonedero = new Command(btn_AceptarPopupMonedero);
            btnBuscarMonederoPopUp = new Command(btn_BuscarMonederoPopUp);
            btnMonederoOK = new Command<Datos.Carrito.Response.ConsultaSocioFrecuenteDetalle>(btn_MonederoOK);
            PopupBusquedaMonederoCerrar = new Command(Popup_BusquedaMonedero_Cerrar);
            BGColor_TotalPagar = Color.FromRgb(255, 0, 0);  //ROJO

            AgregarPago = new Command<TblPayment>(App.Funciones.AgregarAPago);

            getPagos();

            NumTarjeta = "";
            MontoPagoTB = "";
            NombreTarjetahabiente = "";

            TabActivo = new Command(Tab_Activo);
            BGColor_TabEfectivo = Color.FromRgb(16, 108, 7); //Color.Green  #106c07
            TabEfectivoVisible = true;
            BGColor_TabTarjeta = Color.FromRgb(52, 73, 94); //Color Azul  #34495E
            TabTarjetaVisible = true;
            BGColor_TabMonedero = Color.FromRgb(52, 73, 94); //Color Azul #34495E
            TabMonederoVisible = true;

            List<TblGeneralesVenta> datosGrales = App.ServiciosBD.ObtenerListaEntidadLocal("TblGeneralesVenta").OfType<TblGeneralesVenta>().ToList();
            DGV_Documento = datosGrales[0].NombreDocumento == null ? "" : datosGrales[0].NombreDocumento;
            DGV_Monedero = datosGrales[0].NumMonedero == "" ? "SIN MONEDERO" : datosGrales[0].NumMonedero;
        }

        /// <summary>
        /// Se busca el Número del Monedero con el dato indicado en el Filtro
        /// </summary>
        private async void btn_BuscarMonederoPopUp()
        {
            string filtro = await App.Current.MainPage.DisplayPromptAsync("Búsqueda de Monedero", "Indica el dato por el cual se buscará el Monedero. Por ejemplo: Nombre, Tarjeta, Teléfono o Correo", "Aceptar", "Cancelar", "Dato de Búsqueda", 50, keyboard: Keyboard.Text, "");

            try
            {
                if (filtro != null && filtro != "")
                {
                    PopupEspera("Buscando...");
                    ApiDataStore DStore = new();
                    var ResBusqueda = await DStore.Consulta_SocioFrecuente(1, 1, 1, filtro);

                    if (ResBusqueda != null)
                    {
                        monederoList.Clear();
                        foreach (var member in ResBusqueda.DataL)
                        {
                            Datos.Carrito.Response.ConsultaSocioFrecuenteDetalle Info = new(member.Correo.ToString(), member.Nombre.ToString(), int.Parse(member.Socio.ToString()), member.Tarjeta.ToString(), member.Telefono.ToString());
                            monederoList.Add(Info);
                        }
                        PopupCerrar();
                    }
                    else
                    {
                        PopupCerrar();
                        var toast = Toast.Make("No se encontró un Monedero con el dato indicado.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                        await toast.Show();
                        return;
                    }
                }
            }
            catch (Exception)
            {
                PopupCerrar();
                var toast = Toast.Make("Error al obtener la información del Monedero.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
                return;
            }
        }

        /// <summary>
        /// Se agrega el Número del monedero seleccionado a la ventana PopUp
        /// </summary>
        /// <param name="obj">Objeto del Monedero seleccionado</param>
        private async void btn_MonederoOK(Datos.Carrito.Response.ConsultaSocioFrecuenteDetalle obj)
        {
            try
            {
                NumMonederoLealtadPopUp = obj.Tarjeta;

                ApiDataStore DStore = new();
                var ResDetalle = await DStore.Consulta_DetalleSocioFrecuente(int.Parse(App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblConfiguracion", "EMPRESA_VIRTUAL", "Valor")), int.Parse(App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblConfiguracion", "SUCURSAL_VIRTUAL", "Valor")), int.Parse(App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblConfiguracion", "CAJA_VIRTUAL", "Valor")), obj.Socio);
                
                if (ResDetalle != null)
                {
                    SaldoMonedero = ResDetalle.DataL[0].Saldo;
                    IdSocio_Monedero = ResDetalle.DataL[0].Id;

                    if (NuevoMonedero)
                    {
                        List<TblGeneralesVenta> datos = App.ServiciosBD.ObtenerListaEntidadLocal(new TblGeneralesVenta().GetType().Name).OfType<TblGeneralesVenta>().ToList();
                        TblGeneralesVenta datosGenerales = new();
                        datosGenerales.IdUsuario = Convert.ToInt32(App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblConfiguracion", "CLIENTE_VIRTUAL", "Valor"));
                        datosGenerales.IdTipoDocumento = datos[0].IdTipoDocumento;
                        datosGenerales.NombreDocumento = datos[0].NombreDocumento;
                        datosGenerales.IdTipoCliente = datos[0].IdTipoCliente;
                        datosGenerales.NombreCliente = datos[0].NombreCliente;
                        datosGenerales.NumMonedero = obj.Tarjeta;
                        datosGenerales.SaldoMonedero = SaldoMonedero;
                        datosGenerales.PTarjeta = datos[0].PTarjeta;
                        datosGenerales.Transaccion = datos[0].Transaccion;
                        App.ServiciosBD.ActualizarRegistroEntidadLocal(datosGenerales);
                        DGV_Monedero = App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblGeneralesVenta", App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblConfiguracion", "CLIENTE_VIRTUAL", "Valor"), "NumMonedero") == null ? "SIN MONEDERO" : App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblGeneralesVenta", App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblConfiguracion", "CLIENTE_VIRTUAL", "Valor"), "NumMonedero");
                    }
                }
                else
                {
                    var toast = Toast.Make("No se encontró el detalle de Monedero.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                    return;
                }
            }
            catch (Exception)
            {
                var toast = Toast.Make("Error al obtener el detalle del Monedero.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
                return;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Popup_Monedero_Cerrar()
        {
            monederoList.Clear();
            NuevoMonedero = false;
        }
               
        /// <summary>
        /// Al Cancelar se borra la compra
        /// </summary>
        private async void btn_Cancelar()
        {
            bool answer = await App.Current.MainPage.DisplayAlert("Aviso", "Desea cancelar la compra?", "Si", "No");
            if (answer == true)
            {
                App.ServiciosBD.LimpiarEntidadLocal(new TblCarrito());
                App.ServiciosBD.LimpiarEntidadLocal(new TblPayment());
                App.ServiciosBD.LimpiarEntidadLocal(new TblGeneralesVenta());
                App.ServiciosBD.LimpiarEntidadLocal(new TblArticulosCupones());
                //EFO
                //await Shell.Current.GoToAsync("Iniciopage");
                await Shell.Current.GoToAsync("Carritopage");
                
            }
        }

        /// <summary>
        /// Se obtienen las Formas de Pagos registradas en la compra actual
        /// </summary>
        public async void getPagos()
        {
            try
            {
                PopUpVisible = false;
                TblPayment Pay = new();
                List<TblPayment> pagos = App.ServiciosBD.ObtenerListaEntidadLocal("TblPayment").OfType<TblPayment>().ToList();
                foreach (var pago in pagos)
                {
                    Pay = ((TblPayment)pago);
                    switch (Pay.TipoPago)
                    {
                        case "Efectivo":
                            Total_Efectivo = Pay.MontoCapturado;
                            digitos = Pay.MontoCapturado;
                            break;
                        case "Tarjeta Bancaria":
                            Total_TarjetaBancaria = Pay.MontoCapturado;
                            numTarjeta = Pay.NumTarjeta;                            
                            break;
                        default:
                            break;
                    }
                    Actualizar_MontoTotalCapturado();
                }

            }
            catch (Exception ex)
            {
                var toast = Toast.Make("Error al cargar los montos capturados. ERR: " + ex.Message, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
        }

        /// <summary>
        /// Borra todas las Formas de Pago registradas en la compra actual
        /// </summary>
        public async void BorrarTodoPagos()
        {
            try
            {
                App.ServiciosBD.LimpiarEntidadLocal(new TblPayment());
            }
            catch (Exception ex)
            {
                var toast = Toast.Make("Error al limpiar las Formas de Pago. ERR: " + ex.Message, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
        }

        /// <summary>
        /// Actualiza el Monto la Forma de Pago con la al presionara una cantidad $20, $50, etc.
        /// </summary>
        /// <param name="valor"></param>
        public void Efectivo_Cantidad(object valor)
        {
            TblPayment Pay = new();

            digitos = Math.Round(float.Parse(valor.ToString()), 2).ToString("N2");
            Total_Efectivo = Math.Round(float.Parse(digitos.ToString()), 2).ToString("N2");

            Actualizar_MontoTotalCapturado();

            Pay.IdCarrito = 1;
            Pay.TipoPago = "Efectivo";
            Pay.MontoPagado = Total_EfectivoPagado;
            Pay.MontoCapturado = Total_Efectivo;
            Pay.TotalMontoCapturado = Total_MontoCapturado;
            Pay.IdBanco = 0;
            Pay.NombreBanco = "";
            Pay.NumTarjeta = "";
            Pay.NumVoucher = "";
            Pay.Ticket = 0;

            //MODIFICADO PARA NUEVO CREAR_VENTA()
            App.Funciones.AgregarAPago(Pay);
        }

        /// <summary>
        /// Actualiza el Monto de la Forma de Pago al presionar un digito 1,2,3, etc
        /// </summary>
        /// <param name="valor"></param>
        private void Efectivo_Digito(object valor)
        {
            TblPayment Pay = new();

            if (valor.ToString() == ".")
                return;

            if (digitos == "")
                digitos = valor.ToString();
            else
                digitos = long.Parse(digitos.Replace(".", "").Replace(",", "") + valor.ToString()).ToString();

            if (digitos.Length == 1)
                Total_Efectivo = "0.0" + digitos;
            else if (digitos.Length == 2)
                Total_Efectivo = "0." + digitos;
            else if (digitos.Length >= 3)
                Total_Efectivo = digitos.Substring(0, digitos.Length - 2) + "." + digitos.Substring(digitos.Length - 2);

            Actualizar_MontoTotalCapturado();

            Pay.IdCarrito = 1;
            Pay.TipoPago = "Efectivo";
            Pay.MontoPagado = Total_EfectivoPagado;
            Pay.MontoCapturado = Total_Efectivo;
            Pay.TotalMontoCapturado = Total_MontoCapturado;
            Pay.IdBanco = 0;
            Pay.NombreBanco = "";
            Pay.NumTarjeta = "";
            Pay.NumVoucher = "";
            Pay.Ticket = 0;
            App.Funciones.AgregarAPago(Pay);
        }

        /// <summary>
        /// Actualiza el Monto de la Forma de Pago al presionar el boton para borrar un digito
        /// </summary>
        /// <param name="valor"></param>
        private void Efectivo_BorrarDigito(object valor)
        {
            TblPayment Pay;
            valor = valor.ToString().Replace(",", "");

            if (Total_Efectivo.Length == 0 || Total_Efectivo.Length == 1 || Total_Efectivo == "0.00")
            {
                Total_Efectivo = "0";
                Actualizar_MontoTotalCapturado();
                Pay = new();
                Pay.IdCarrito = 1;
                Pay.TipoPago = "Efectivo";
                Pay.MontoPagado = Total_EfectivoPagado;
                Pay.MontoCapturado = Total_Efectivo;
                Pay.TotalMontoCapturado = Total_MontoCapturado;
                Pay.IdBanco = 0;
                Pay.NombreBanco = "";
                Pay.NumTarjeta = "";
                Pay.NumVoucher = "";
                Pay.Ticket = 0;

                App.ServiciosBD.EliminarRegistroEntidadLocal(Pay);
            }
            else
            {
                if (digitos.Contains("."))
                    digitos = digitos.Replace(".", "");

                if (digitos.Length > 1)
                {
                    digitos = digitos.Substring(0, digitos.Length - 1);
                    if (digitos.Length == 1)
                        Total_Efectivo = "0.0" + digitos;
                    else if (digitos.Length == 2)
                        Total_Efectivo = "0." + digitos;
                    else if (digitos.Length >= 3)
                        Total_Efectivo = digitos.Substring(0, digitos.Length - 2) + "." + digitos.Substring(digitos.Length - 2);
                }
                else
                {
                    digitos = "0";
                    Total_Efectivo = digitos;
                }

                Actualizar_MontoTotalCapturado();

                Pay = new();
                Pay.IdCarrito = 1;
                Pay.TipoPago = "Efectivo";
                Pay.MontoPagado = Total_EfectivoPagado;
                Pay.MontoCapturado = Total_Efectivo;
                Pay.TotalMontoCapturado = Total_MontoCapturado;
                Pay.IdBanco = 0;
                Pay.NombreBanco = "";
                Pay.NumTarjeta = "";
                Pay.NumVoucher = "";
                Pay.Ticket = 0;

                App.ServiciosBD.ActualizarRegistroEntidadLocal(Pay);
            }
        }

        /// <summary>
        /// Actualiza el Monto Total Capturado con la suma de los Montos de cada Forma de Pago
        /// </summary>
        private void Actualizar_MontoTotalCapturado()
        {
            double MontoCapturado = 0;
            double TotalCarrito = 0;
            Total_MontoCapturado = (double.Parse(Total_Efectivo.ToString().Trim()) + double.Parse(Total_TarjetaBancaria.ToString().Trim()) + double.Parse(Total_Monedero.ToString().Trim())).ToString("N2");
            Total_MontoRestante = (double.Parse(Total.ToString().Trim()) - double.Parse(Total_MontoCapturado.ToString().Trim())).ToString("N2");

            MontoCapturado = double.Parse(Total_MontoCapturado.Replace("MXN", "").Trim());
            TotalCarrito = double.Parse(Total.Replace("$", "").Replace("MXN", "").Trim());
            if (MontoCapturado >= TotalCarrito)
            {
                BGColor_TotalPagar = Color.FromRgb(0, 128, 0); //VERDE
                OnPropertyChanged();
            }
            else
            {
                BGColor_TotalPagar = Color.FromRgb(255, 0, 0);  //ROJO
                OnPropertyChanged();
            }
        }       

        /// <summary>
        /// Se hace el cobro de la Venta
        /// </summary>
        private async void btn_Cobrar()
        {
            if (float.Parse(Total_MontoCapturado) < float.Parse(Total))
            {
                var toast = Toast.Make("No se ha ingresado el monto completo", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
                return;
            }

            if (float.Parse(Total_TarjetaBancaria) > 0)
            {
                if (NumTarjeta.ToString() == "")
                {
                    var toast = Toast.Make("Necesita ingresar los últimos 4 dígitos de la Tarjeta.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                    return;
                }

                if (NombreTarjetahabiente == null)
                {
                    var toast = Toast.Make("Necesita indicar el Nombre del Tarjetahabiente.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                    return;
                }
            }

            PopupEspera("Procesando ...");
            BorrarTodoPagos();
            ApiDataStore Api = new();
            TblPayment Pay = new();

            for (int i = 0; i < 3; i++)
            {
                switch (i)
                {
                    case 0:
                        if (float.Parse(Total_Efectivo) > 0)
                        {
                            Pay.IdCarrito = 1;
                            Pay.TipoPago = "Efectivo";
                            Pay.MontoPagado = Total_EfectivoPagado;
                            Pay.MontoCapturado = Total_Efectivo;
                            Pay.TotalMontoCapturado = Total_MontoCapturado;
                            Pay.IdBanco = 0;
                            Pay.NombreBanco = "";
                            Pay.NumTarjeta = "";
                            Pay.NumVoucher = "";
                            Pay.Ticket = 0;
                            App.Funciones.AgregarAPago(Pay);
                        }
                        break;

                    case 1:
                        if (float.Parse(Total_TarjetaBancaria) > 0)
                        {
                            Pay.IdCarrito = 1;
                            Pay.TipoPago = "Tarjeta Bancaria";
                            Pay.MontoPagado = Total_EfectivoPagado;
                            Pay.MontoCapturado = Total_TarjetaBancaria;
                            Pay.TotalMontoCapturado = Total_MontoCapturado;
                            Pay.IdBanco = 0;
                            Pay.NombreBanco = "";
                            Pay.NumTarjeta = NumTarjeta;
                            Pay.NumVoucher = "";
                            Pay.Ticket = 0;
                            App.Funciones.AgregarAPago(Pay);
                        }
                        break;
                    default:
                        break;
                }
            }
            PopupCerrar();

            try
            {
                App.Funciones.crear_Venta(IsSelected_Redimir, Total_MontoCapturado, Total, Total_Monedero, "", "", "", Promociones);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }        

        /// <summary>
        /// Modifica el color de las pestañas de la Forma de Pago para mostrar cual ha sido seleccionada
        /// </summary>
        /// <param name="NomPestana">Nombre de la pestaña seleccionada</param>
        private async void Tab_Activo(object NomPestana)
        {
            switch (NomPestana)
            {
                case "Efectivo":
                    BGColor_TabEfectivo = Color.FromRgb(16, 108, 7); //Color.Green  #106c07
                    TabEfectivoVisible = true;
                    BGColor_TabMonedero = Color.FromRgb(52, 73, 94); //Color Azul  #34495E
                    TabMonederoVisible = false;
                    PopupEfectivo();

                    break;

                case "Tarjeta Bancaria":
                    if (float.Parse(Total_MontoCapturado) >= float.Parse(Total) && float.Parse(Total_TarjetaBancaria) <= float.Parse(Total))
                    {
                        if (float.Parse(Total_TarjetaBancaria) <= 0)
                        {
                            await App.Current.MainPage.DisplayAlert("Aviso", "Ya se cubrió el total de la venta con otra forma de pago, para cambiarla, primero debe quitar la forma de pago anterior.", "Aceptar");
                            return;
                        }
                    }

                    if (float.Parse(Total_MontoCapturado) >= float.Parse(Total) && float.Parse(Total_TarjetaBancaria) >= float.Parse(Total))
                    {
                        await App.Current.MainPage.DisplayAlert("Aviso", "Ya no se puede cambiar la forma de pago, cuando se pagó con Tarjeta Bancaria.", "Aceptar");
                        return;
                    }

                    BGColor_TabEfectivo = Color.FromRgb(52, 73, 94); //Color Azul  #34495E
                    TabEfectivoVisible = false;
                    BGColor_TabTarjeta = Color.FromRgb(16, 108, 7); //Color.Green  #106c07
                    TabTarjetaVisible = true;                    
                    BGColor_TabMonedero = Color.FromRgb(52, 73, 94); //Color Azul  #34495E
                    TabMonederoVisible = false;
                    PopupTarjetaBancaria();

                    break;

                case "Monedero":                    
                    BGColor_TabEfectivo = Color.FromRgb(52, 73, 94); //Color Azul  #34495E
                    TabEfectivoVisible = false;
                    BGColor_TabMonedero = Color.FromRgb(16, 108, 7); //Color.Green  #106c07
                    TabMonederoVisible = true;
                    PopupMonedero();

                    break;

                default:
                    break;
            }

            //Se modifica el color de la pestaña, se borra la lista de las pestañas y se vuelve a cargar para mostrar el cambio de color en la vista
            ObservableCollection<FormasPago_Visual> nuevaLista = new();
            foreach (var pago in Pestana_FormaPagoList)
            {
                if (pago.EtiquetaTab == NomPestana.ToString())
                    pago.BGColor_Tab = Color.FromRgb(16, 108, 7); //Color.Green  #106c07
                else
                    pago.BGColor_Tab = Color.FromRgb(52, 73, 94); //Color Azul  #34495E

                nuevaLista.Add(new FormasPago_Visual { ID = pago.ID, NombreTab = pago.NombreTab, EtiquetaTab = pago.EtiquetaTab, Nombre1 = pago.Nombre1, Nombre2 = pago.Nombre2, BGColor_Tab = pago.BGColor_Tab, Icono = pago.Icono, IsVisibleTab = pago.IsVisibleTab, MontoTab = pago.MontoTab != null ? pago.MontoTab : "0.00" });
            }

            Pestana_FormaPagoList.Clear();
            Pestana_FormaPagoList = nuevaLista;
        }        

        /// <summary>
        /// Se crea la bonificación al Monedero capturado
        /// </summary>
        /// <param name="vTicket"></param>
        /// <returns></returns>
        public async Task<bool> Crear_BonificacionMonedero(int vTicket)
        {
            bool BonificacionExitoso = false;
            try
            {
                ApiDataStore Api = new();
                string pOrigen = "PV";
                bool pNo_Acumula_Lealtad = false;
                int pAutorizador_Id = 3; // Valor Fijo
                int pTicket = vTicket;
                string pProductos = ""; //0001 750112510479400003330 

                List<object> pListaFormaPago = new List<object>();
                pListaFormaPago.Add(new BonificacionMonederoFormaPago() { FormaPago_Id = 1, FormaPago_Monto = Total.Replace(",", "") });
                var jsonFormaPago = Newtonsoft.Json.JsonConvert.SerializeObject(pListaFormaPago);


                //pProductos = debe ir de la siguiente manera cantidad 4 campos rellenos de 0(Format(Cantidad, "0000"))
                //             Articulo 14 campos espacio a la izquierda(RellenaEspacioIzquierda(Articulo, 14))
                //             Monto de línea 8 campos rellenos de 0(Format(_MontoLinea * 100, "00000000"))
                //             pListaFormaPago = Vacio

                List<TblCarrito> CartArt = App.ServiciosBD.ObtenerListaEntidadLocal(new TblCarrito().GetType().Name).OfType<TblCarrito>().ToList();
                foreach (TblCarrito producto in CartArt)
                {
                    pProductos += producto.Piezas.ToString("D4") + producto.SkuArticulo.PadLeft(14) + (long.Parse(producto.TotalProducto.ToString()) * 100).ToString("D8");
                }

                //EFO
                //var ResBonificacionMonedero = await Api.ObtenerBonificacionMonedero(App.MiAperturaService.ConsultaCaja("emp"), pAutorizador_Id, App.MiAperturaService.ConsultaCaja("suc"), App.MiAperturaService.ConsultaCaja("caja"), pTicket, long.Parse(App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblGeneralesVenta", "NombreDocumento", "Valor") == null ? "" : App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblGeneralesVenta", "NumMonedero", "Valor")), App.MiLoginService.ConsultaElemento("Usuario_Id"), decimal.Parse(Total_Monedero.Replace(",", "")), pOrigen, pProductos, int.Parse(App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblGeneralesVenta", "NombreDocumento", "Valor") == null ? "" : App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblGeneralesVenta", "TipoDocumento", "Valor")), App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblGeneralesVenta", "NombreDocumento", "Valor") == null ? "" : App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblGeneralesVenta", "TipoCliente", "Valor"), pNo_Acumula_Lealtad, jsonFormaPago);
                //var ResBonificacionMonedero = await Api.ObtenerBonificacionMonedero(1, pAutorizador_Id, 1, 1, pTicket, long.Parse(App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblGeneralesVenta", "NombreDocumento", "Valor") == null ? "" : App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblGeneralesVenta", "NumMonedero", "Valor")), "100001", decimal.Parse(Total_Monedero.Replace(",", "")), pOrigen, pProductos, int.Parse(App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblGeneralesVenta", "NombreDocumento", "Valor") == null ? "" : App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblGeneralesVenta", "TipoDocumento", "Valor")), App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblGeneralesVenta", "NombreDocumento", "Valor") == null ? "" : App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblGeneralesVenta", "TipoCliente", "Valor"), pNo_Acumula_Lealtad, jsonFormaPago);
                var ResBonificacionMonedero = await Api.ObtenerBonificacionMonedero(int.Parse(App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblConfiguracion", "EMPRESA_VIRTUAL", "Valor")), pAutorizador_Id, int.Parse(App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblConfiguracion", "SUCURSAL_VIRTUAL", "Valor")), int.Parse(App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblConfiguracion", "CAJA_VIRTUAL", "Valor")), pTicket, long.Parse(App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblGeneralesVenta", App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblConfiguracion", "CLIENTE_VIRTUAL", "Valor"), "NumMonedero") == null ? "" : App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblGeneralesVenta", App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblConfiguracion", "CLIENTE_VIRTUAL", "Valor"), "NumMonedero")), App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblConfiguracion", "CLIENTE_VIRTUAL", "Valor"), decimal.Parse(Total_Monedero.Replace(",", "")), pOrigen, pProductos, int.Parse(App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblGeneralesVenta", App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblConfiguracion", "CLIENTE_VIRTUAL", "Valor"), "TipoDocumento") == null ? "" : App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblGeneralesVenta", App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblConfiguracion", "CLIENTE_VIRTUAL", "Valor"), "TipoDocumento")), App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblGeneralesVenta", App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblConfiguracion", "CLIENTE_VIRTUAL", "Valor"), "NombreDocumento") == null ? "" : App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblGeneralesVenta", App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblConfiguracion", "CLIENTE_VIRTUAL", "Valor"), "NombreDocumento"), pNo_Acumula_Lealtad, jsonFormaPago);

                if (ResBonificacionMonedero != null && !ResBonificacionMonedero.Equals(""))
                {
                    BonificacionExitoso = ResBonificacionMonedero.DataL[0].Exitoso;
                    BonificacionMonedero_Autorizacion = ResBonificacionMonedero.DataL[0].Autorizacion;
                }

                return BonificacionExitoso;
            }
            catch (Exception ex)
            {
                var toast = Toast.Make("Error al hacer la Bonificación al Monedero.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
                return BonificacionExitoso;
            }
        }

        /// <summary>
        /// Se obtiene el monto del Monedero capturado
        /// </summary>
        public async void Obtener_MontoMonedero()
        {
            try
            {
                MensajeMonedero = "";

                if (App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblGeneralesVenta", App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblConfiguracion", "CLIENTE_VIRTUAL", "Valor"), "NumMonedero") != null)
                {
                    if (IsSelected_Redimir == true)
                    {
                        if (double.Parse(Total_MontoCapturado) < double.Parse(Total))
                        {
                            string saldo = App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblGeneralesVenta", App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblConfiguracion", "CLIENTE_VIRTUAL", "Valor"), "SaldoMonedero") == null ? "0.00" : App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblGeneralesVenta", App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblConfiguracion", "CLIENTE_VIRTUAL", "Valor"), "SaldoMonedero").ToString();
                            if (double.Parse(saldo) == 0)
                            {
                                Total_Monedero = "0.00";
                                MensajeMonedero = "El saldo del Monedero es de $ 0.00 ";
                                IsSelected_Redimir = false;
                            }
                            else
                            {
                                double faltante = double.Parse(((float.Parse(SubTotal) - float.Parse(Descuentos) - float.Parse(Promociones) - float.Parse(Total_Efectivo) - float.Parse(Total_Monedero))).ToString());
                                bool answer = await App.Current.MainPage.DisplayAlert("Monedero", "¿Quiere redimir el saldo completo del Monedero?, el saldo actual es de: $ " + saldo + "?", "Si", "No");
                                if (answer == true)
                                {
                                    if (double.Parse(saldo) < faltante)
                                    {
                                        Total_Monedero = saldo;
                                        MensajeMonedero = "El saldo del Monedero solo es de: $ " + saldo;
                                    }
                                    else
                                    {
                                        Total_Monedero = faltante.ToString();
                                        MensajeMonedero = "";
                                    }
                                }
                                else
                                {
                                    string result = await App.Current.MainPage.DisplayPromptAsync("Monedero", "Indique el monto que será Redimido del Monedero", "Aceptar", "Cancelar", initialValue: "0", keyboard: Keyboard.Numeric);
                                    if (String.IsNullOrEmpty(result))
                                        result = "0";
                                    Total_Monedero = result.ToString(); ;
                                    MensajeMonedero = "";
                                }

                            }

                            Actualizar_MontoTotalCapturado();
                        }
                        else
                        {
                            IsSelected_Redimir = false;
                            await App.Current.MainPage.DisplayAlert("Pago", "Ya se cuenta con el monto completo a pagar, si desea aplicar el saldo del Monedero necesita modificar alguna otra forma de pago", "Aceptar");
                        }
                    }
                    else
                    {
                        Total_Monedero = "0.00";
                        MensajeMonedero = "";
                        Actualizar_MontoTotalCapturado();
                    }
                }
                else
                {
                    IsSelected_Redimir = false;
                    await App.Current.MainPage.DisplayAlert("Pago", "No puedes redimir ya que no ingresaste ningún Monedero.", "Aceptar");
                }
                return;
            }
            catch (Exception)
            {
                var toast = Toast.Make("Error al verificar el monto del Monedero.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
                return;
            }
        }

        /// <summary>
        /// Al dar click en el botón Aceptar del Popup de Efectivo
        /// </summary>
        private void btn_AceptarPopupEfectivo()
        {
            popEfectivo.CerrarPopup();

            //Se modifica el color de la pestaña, se borra la lista de las pestañas y se vuelve a cargar para mostrar el cambio de color en la vista
            ObservableCollection<FormasPago_Visual> nuevaLista = new();
            foreach (var pago in Pestana_FormaPagoList)
            {
                if (pago.EtiquetaTab == "Efectivo")
                {
                    nuevaLista.Add(new FormasPago_Visual { ID = pago.ID, NombreTab = pago.NombreTab, EtiquetaTab = pago.EtiquetaTab, Nombre1 = pago.Nombre1, Nombre2 = pago.Nombre2, BGColor_Tab = pago.BGColor_Tab, Icono = pago.Icono, IsVisibleTab = pago.IsVisibleTab, MontoTab = cantidadEfectivo });
                }
                else
                {
                    nuevaLista.Add(new FormasPago_Visual { ID = pago.ID, NombreTab = pago.NombreTab, EtiquetaTab = pago.EtiquetaTab, Nombre1 = pago.Nombre1, Nombre2 = pago.Nombre2, BGColor_Tab = pago.BGColor_Tab, Icono = pago.Icono, IsVisibleTab = pago.IsVisibleTab, MontoTab = pago.MontoTab != null ? pago.MontoTab : "0.00" });
                }
            }

            Pestana_FormaPagoList.Clear();
            Pestana_FormaPagoList = nuevaLista;
        }

        /// <summary>
        /// Al dar click en el botón Aceptar del Popup de Tarjeta Bancaria
        /// </summary>
        private async void btn_AceptarPopupTarjetaBancaria()
        {
            if (montoPagoTB.Trim() == "" &&  numTarjeta.Trim() == "" && nombreTarjetahabiente.Trim() == "")
            {
                popTarjetaBancaria.CerrarPopup();

                //Se modifica el color de la pestaña, se borra la lista de las pestañas y se vuelve a cargar para mostrar el cambio de color en la vista
                ObservableCollection<FormasPago_Visual> nuevaLista = new();
                foreach (var pago in Pestana_FormaPagoList)
                {
                    if (pago.EtiquetaTab == "Tarjeta Bancaria")
                    {
                        nuevaLista.Add(new FormasPago_Visual { ID = pago.ID, NombreTab = pago.NombreTab, EtiquetaTab = pago.EtiquetaTab, Nombre1 = pago.Nombre1, Nombre2 = pago.Nombre2, BGColor_Tab = pago.BGColor_Tab, Icono = pago.Icono, IsVisibleTab = pago.IsVisibleTab, MontoTab = cantidadTarjetaBancaria });
                    }
                    else
                    {
                        nuevaLista.Add(new FormasPago_Visual { ID = pago.ID, NombreTab = pago.NombreTab, EtiquetaTab = pago.EtiquetaTab, Nombre1 = pago.Nombre1, Nombre2 = pago.Nombre2, BGColor_Tab = pago.BGColor_Tab, Icono = pago.Icono, IsVisibleTab = pago.IsVisibleTab, MontoTab = pago.MontoTab != null ? pago.MontoTab : "0.00" });
                    }
                }

                Pestana_FormaPagoList.Clear();
                Pestana_FormaPagoList = nuevaLista;
            }
            else
            {
                if (montoPagoTB == "")
                {
                    var toast = Toast.Make("Necesitas ingresar el monto a pagar con la Tarjeta", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                    return;
                }

                if (numTarjeta == "")
                {
                    var toast = Toast.Make("Necesitas ingresar el los dígitos de la Tarjeta", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                    return;
                }

                if (nombreTarjetahabiente == "")
                {
                    var toast = Toast.Make("Necesitas ingresar el Nombre del Tarjetahabiente", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                    return;
                }

                Total_TarjetaBancaria = montoPagoTB;
                Actualizar_MontoTotalCapturado();
                popTarjetaBancaria.CerrarPopup();
            }
        }

        /// <summary>
        /// Al dar click en el botón Aceptar del Popup de Monedero
        /// </summary>
        private void btn_AceptarPopupMonedero()
        {
            popMonedero.CerrarPopup();

            //Se modifica el color de la pestaña, se borra la lista de las pestañas y se vuelve a cargar para mostrar el cambio de color en la vista
            ObservableCollection<FormasPago_Visual> nuevaLista = new();
            foreach (var pago in Pestana_FormaPagoList)
            {
                if (pago.EtiquetaTab == "Monedero")
                {
                    nuevaLista.Add(new FormasPago_Visual { ID = pago.ID, NombreTab = pago.NombreTab, EtiquetaTab = pago.EtiquetaTab, Nombre1 = pago.Nombre1, Nombre2 = pago.Nombre2, BGColor_Tab = pago.BGColor_Tab, Icono = pago.Icono, IsVisibleTab = pago.IsVisibleTab, MontoTab = cantidadMonedero });
                }
                else
                {
                    nuevaLista.Add(new FormasPago_Visual { ID = pago.ID, NombreTab = pago.NombreTab, EtiquetaTab = pago.EtiquetaTab, Nombre1 = pago.Nombre1, Nombre2 = pago.Nombre2, BGColor_Tab = pago.BGColor_Tab, Icono = pago.Icono, IsVisibleTab = pago.IsVisibleTab, MontoTab = pago.MontoTab != null ? pago.MontoTab : "0.00" });
                }
            }

            Pestana_FormaPagoList.Clear();
            Pestana_FormaPagoList = nuevaLista;
        }
        
    }
}

