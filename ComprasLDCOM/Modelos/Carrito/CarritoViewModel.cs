using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using ComprasLDCOM.Popups.Carrito;
using ComprasLDCOM.Datos.Carrito.Response;
using ComprasLDCOM.Datos.Carrito.BaseDeDatos;
using ComprasLDCOM.Datos.Inicio.Request;
using ComprasLDCOM.Popups.General;
using ComprasLDCOM.API;
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
using ComprasLDCOM.Modelos.Inicio;

namespace ComprasLDCOM.Modelos.Carrito
{
    internal class CarritoViewModel : BindableObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        //Se debe poner ObservableCollection, para que me permita conectar a la vista y se observe en la pantalla
        #region ObservableCollection

        /// <summary>
        /// Lista de los Productos agregados al Carrito
        /// </summary>
        public ObservableCollection<Datos.Inicio.Request.Articulo> _productsList { get; set; } = new();
        
        /// <summary>
        /// Lista de los Productos agregados al Carrito
        /// </summary>
        public ObservableCollection<Datos.Inicio.Request.Articulo> productsList
        {
            get { return _productsList; }
            set
            {
                _productsList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(productsList)));
            }
        }
        
        /// <summary>
        /// Monto Total de la Venta
        /// </summary>
        public ObservableCollection<TotalVenta> _totalVenta { get; set; } = new();
        
        /// <summary>
        /// Monto Total de la Venta
        /// </summary>
        public ObservableCollection<TotalVenta> TotalVenta
        {
            get { return _totalVenta; }
            set
            {
                _totalVenta = value;
                OnPropertyChanged("TotalVenta");
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalVenta)));
            }
        }

        /// <summary>
        /// Lista del Monedero consultado
        /// </summary>
        private ObservableCollection<Datos.Carrito.Response.ConsultaSocioFrecuenteDetalle> _monederoList { get; set; } = new();
        
        /// <summary>
        /// Lista del Monedero consultado
        /// </summary>
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


        #region BindableProperty        
        /// <summary>
        /// Propiedad para el Total de la Venta
        /// </summary>
        public BindableProperty TotalVentaProperty = BindableProperty.Create(nameof(TotalVenta), typeof(string), typeof(CarritoViewModel));
        
        /// <summary>
        /// Propiedad para el Número de Artículos agregados en el Carrito
        /// </summary>
        public BindableProperty CountCartProperty = BindableProperty.Create(nameof(CountCart), typeof(int), typeof(CarritoViewModel));
        
        /// <summary>
        /// Propiedad para el Texto que se muestra en el Overlay de Espera
        /// </summary>
        public BindableProperty TextoOverlayProperty = BindableProperty.Create(nameof(TextoOverlay), typeof(string), typeof(CarritoViewModel));
        
        /// <summary>
        /// Propiedad para el Número del Monedero agregado en la Venta
        /// </summary>
        public BindableProperty numMonederoProperty = BindableProperty.Create(nameof(NumMonederoLealtadPopUp), typeof(string), typeof(CarritoViewModel));
        
        /// <summary>
        /// Propiedad para el poner el color al seleccionar Entrega a Domicilio
        /// </summary>
        public BindableProperty ColorEntregaDomicilioProperty = BindableProperty.Create(nameof(ColorEntregaDomicilio), typeof(Color), typeof(CarritoViewModel));
        
        /// <summary>
        /// Propiedad para el poner el color al seleccionar Click_Collect
        /// </summary>
        public BindableProperty ColorEntregaTiendaProperty = BindableProperty.Create(nameof(ColorEntregaTienda), typeof(Color), typeof(CarritoViewModel));

        public BindableProperty IsVisibleImagenBuscarProperty = BindableProperty.Create(nameof(IsVisibleImagenBuscar), typeof(bool), typeof(CarritoViewModel));

        #endregion


        #region Variables_Propiedades
        ApiDataStore DStore = new();
        bool NuevoMonedero = false;

        /// <summary>
        /// Variable para el PopUp de Espera
        /// </summary>
        PopupPageEspera popEspera = null;
        
        /// <summary>
        /// Variable para el Popup de Datos Generales
        /// </summary>
        PopupPageGeneralesVenta PopupGeneralesVenta = null;

        /// <summary>
        /// Variable para el Popup de Monedero
        /// </summary>
        PopupPageMonedero PopupMonedero = null;

        /// <summary>
        /// Variable para el Popup de Busqueda Monedero
        /// </summary>
        PopupPageBusquedaMonedero PopupBusquedaMonedero = null;
        
        /// <summary>
        /// Los datos generales para la Venta
        /// </summary>
        TblGeneralesVenta datosGenerales;
              
        /// <summary>
        /// Propiedad para el botón de Servicio a Domicilio en Datos Generales
        /// </summary>
        public ICommand IsCheckedDomicilio { get; }

        /// <summary>
        /// Propiedad para el botón de Click_Collect en Datos Generales
        /// </summary>
        public ICommand IsCheckedTienda { get; }

        /// <summary>
        /// Propiedad para mostrar el PopUp de Búsqueda de Monedero
        /// </summary>
        public ICommand PopupBusquedaMonederoAbrir { get; }

        /// <summary>
        /// Botón para cerrar el Popup de Búsqueda Monedero
        /// </summary>
        public ICommand PopupBusquedaMonederoCerrar { get; }

        /// <summary>
        /// Propiedad para mostrar el PopUp de Monedero
        /// </summary>
        public ICommand PopupMonederoAbrir { get; }


        /// <summary>
        /// Propiedad para cerrar el PopUp de Monedero
        /// </summary>
        public ICommand PopupMonederoCerrar { get; }

        /// <summary>
        /// Propiedad para el botón de Cancelar Venta
        /// </summary>
        public ICommand CancelarVenta { get; }
        
        /// <summary>
        /// Propiedad para el botón de Eliminar Artículo
        /// </summary>
        public ICommand EliminarDelCarrito { get; }
        
        /// <summary>
        /// Propiedad para el botón (+) de agregar piezas
        /// </summary>
        public ICommand AgregarPiezas { get; }
        
        /// <summary>
        /// Propiedad para el botón (-) de disminuir piezas
        /// </summary>
        public ICommand QuitarPiezas { get; }
        
        /// <summary>
        /// Propiedad para el botón Pagar Venta
        /// </summary>
        public ICommand PagarVenta { get; }
        
        /// <summary>
        /// Propiedad para el botón Agregar Artículo
        /// </summary>
        public ICommand AgregarArticulo { get; }
                
        /// <summary>
        /// Botón Aceptar de la Ventana Popup
        /// </summary>
        public ICommand btnDatosGeneralesAceptar { get; }
        
        /// <summary>
        /// Botón Cancelar de la Ventana Popup
        /// </summary>
        public ICommand btnDatosGeneralesCancelar { get; }

        /// <summary>
        /// Propiedad para modificar el número de pizas de un Artículo de forma manual
        /// </summary>
        public ICommand PiezasManuales { get; }
        
        /// <summary>
        /// Variable para el Monto Total del Carrito
        /// </summary>
        float totalCarrito = 0;
        
        /// <summary>
        /// Propiedad para el Monto Total del Carrito
        /// </summary>
        public float Total_Carrito
        {
            get { return totalCarrito; }

            set
            {
                if (value == totalCarrito)
                    return;

                totalCarrito = value;
                OnPropertyChanged();

            }
        }
                
        /// <summary>
        /// Saldo que tiene el monedero seleccionado
        /// </summary>
        private decimal SaldoMonedero = 0;
        
        /// <summary>
        /// Id del Socio del monedero seleccionado
        /// </summary>
        private int IdSocio_Monedero = 0;

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
        /// Variable para identificar si se modificó el número de Piezas manualmente
        /// </summary>
        private bool PzsManuales = false;

        /// <summary>
        /// Propiedad del Número de Artículos en el Carrito
        /// </summary>
        public int CountCart
        {
            get => (int)GetValue(CountCartProperty);
            set => SetValue(CountCartProperty, value);
        }

        /// <summary>
        /// Propiedad para el Texto que se muestra en el Overlay de Espera
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
        /// Botón para buscar un Monedero_Lealtad
        /// </summary>
        public ICommand btnBuscarMonederoPopUp { get; }
        
        /// <summary>
        /// Botón OK del registro del Monedero encontrado en la Ventana Popup para seleccionar el Monedero
        /// </summary>
        public ICommand btnMonederoOK { get; }
         
        /// <summary>
        /// Variable para saber si se seleccionó Entrega a Domicilio
        /// </summary>
        bool IsCheckDomicilio = false;

        /// <summary>
        /// Variable para saber si se seleccionó Recoger en Tienda
        /// </summary>
        bool IsCheckTienda = false;

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
        /// Asignar el color de fondo al botón Entrega a Domicilio
        /// </summary>
        public Color ColorEntregaDomicilio
        {
            get => (Color)GetValue(ColorEntregaDomicilioProperty);
            set
            {
                SetValue(ColorEntregaDomicilioProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ColorEntregaDomicilio)));
            }
        }

        /// <summary>
        /// Asignar el color de fondo al botón Entrefa en Tienda
        /// </summary>
        public Color ColorEntregaTienda
        {
            get => (Color)GetValue(ColorEntregaTiendaProperty);
            set
            {
                SetValue(ColorEntregaTiendaProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ColorEntregaTienda)));
            }
        }

        /// <summary>
        /// Variable para mostrar o no el combo de Sucursales
        /// </summary>
        bool _sucursalVisible;
        
        /// <summary>
        /// Propiedad para mostrar o no el combo de Sucursales
        /// </summary>
        public bool SucursalVisible
        {
            get { return _sucursalVisible; }

            set
            {
                if (SucursalVisible == value)
                    return;
                else
                {
                    _sucursalVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Variable para mostrar o no el grid de Domicilio
        /// </summary>
        bool _domicilioVisible;
        
        /// <summary>
        /// Propiedad para mostrar o no el grid de Domicilio
        /// </summary>
        public bool DomicilioVisible
        {
            get { return _domicilioVisible; }

            set
            {
                if (DomicilioVisible == value)
                    return;
                else
                {
                    _domicilioVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Variable para la lista de Sucursales Click_Collect
        /// </summary>
        public List<TblSucursalClickCollect> _sucClickCollect;
        /// <summary>
        /// Propiedad para la lista de Sucursales Click_Collect
        /// </summary>
        public List<TblSucursalClickCollect> SucClickCollectList
        {
            get { return _sucClickCollect; }
            set
            {
                _sucClickCollect = value;
                OnPropertyChanged();
            }

        }
                
        /// <summary>
        /// Id de la Sucursal Click_Collect
        /// </summary>
        public TblSucursalClickCollect SucClickCollect_Id { get; set; }
        /// <summary>
        /// Sucursal Click_Collect seleccionada
        /// </summary>
        public TblSucursalClickCollect SucClickCollect_IdSelected
        {
            get { return SucClickCollect_Id; }
            set
            {
                if (SucClickCollect_IdSelected != value)
                {

                    SucClickCollect_Id = value;                    
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Para la visibilidad de la imagen de inicio de busqueda
        /// </summary>
        public bool IsVisibleImagenBuscar
        {
            get => (bool)GetValue(IsVisibleImagenBuscarProperty);
            set
            {
                SetValue(IsVisibleImagenBuscarProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsVisibleImagenBuscar)));
            }
        }

        #endregion


        public CarritoViewModel()
        {
            IsVisibleImagenBuscar = false;
            CancelarVenta = new Command<Datos.Inicio.Request.Articulo>(CancelaVenta);
            EliminarDelCarrito = new Command<Datos.Inicio.Request.Articulo>(EliminarArtDelCarrito);
            AgregarPiezas = new Command<Datos.Inicio.Request.Articulo>(AgregarMasPiezas);
            QuitarPiezas = new Command<Datos.Inicio.Request.Articulo>(QuitarMasPiezas);
            PiezasManuales = new Command<Datos.Inicio.Request.Articulo>(PiezaManual);
            PopupBusquedaMonederoAbrir = new Command(Popup_BusquedaMonedero_Abrir);
            PopupBusquedaMonederoCerrar = new Command(Popup_BusquedaMonedero_Cerrar);
            PopupMonederoAbrir = new Command(Popup_Monedero_Abrir);
            PopupMonederoCerrar = new Command(Popup_Monedero_Cerrar);
            PagarVenta = new Command(PagarCarrito);
            AgregarArticulo = new Command(AgregaArticulo);
            btnDatosGeneralesAceptar = new Command(btn_DatosGeneralesAceptar);
            btnDatosGeneralesCancelar = new Command(btn_DatosGeneralesCancelar);
            btnBuscarMonederoPopUp = new Command(btn_BuscarMonederoPopUp);
            btnMonederoOK = new Command<Datos.Carrito.Response.ConsultaSocioFrecuenteDetalle>(btn_MonederoOK);
            IsCheckedDomicilio = new Command(IsCheckedEntregaDomicilio);
            IsCheckedTienda = new Command(IsCheckedEntregaTienda);
            SucursalVisible = false;
            DomicilioVisible = false;
            ObtenCarrito();
            popEspera = null;

            if (CountCart <= 0)
                IsVisibleImagenBuscar = true;
            else
                IsVisibleImagenBuscar = false;

            //Carga_Inicial();
        }         

        //EFO en donde se guardaran los datos?
        private async void Carga_Inicial()
        {
            var ResBusqueda = await DStore.Consulta_SucursalUbicacion(1, 1);
            if (ResBusqueda != null && !ResBusqueda.Equals(""))
            {
                int Emp;
                int Suc;
                int Caja;

                string[] Detalle = ResBusqueda.ToString().Split(",");

                if (Detalle[0].Length > Detalle[0].IndexOf(':') + 1)
                {
                    Emp = int.Parse(Detalle[0].Substring(Detalle[0].IndexOf(':') + 1, Detalle[0].Length - Detalle[0].IndexOf(':') - 1));
                }

                if (Detalle[1].Length > Detalle[1].IndexOf(':') + 1)
                {
                    Suc = int.Parse(Detalle[1].Substring(Detalle[1].IndexOf(':') + 1, Detalle[1].Length - Detalle[1].IndexOf(':') - 1));
                }

                if (Detalle[2].Length > Detalle[2].IndexOf(':') + 1)
                {
                    Caja = int.Parse(Detalle[2].Substring(Detalle[2].IndexOf(':') + 1, Detalle[2].Length - Detalle[2].IndexOf(':') - 3));
                }
            }          
             
        }

        /// <summary>
        /// Obtiene los datos del Carrito
        /// </summary>
        public void ObtenCarrito()
        {
            TextoOverlay = "Cargando ...";
            App.Funciones.PopupEspera();

            productsList.Clear();

            List<TblCarrito> CarDB = App.ServiciosBD.ObtenerListaEntidadLocal(new TblCarrito().GetType().Name).OfType<TblCarrito>().ToList();
            ActualizaTotalVenta(CarDB.Sum(x => Math.Round(x.TotalProducto, 2)).ToString("###,###.#0"), CarDB.Sum(x => x.Piezas), CarDB.Sum(x => (x.DescuentoMonto)).ToString("###,###.#0"), CarDB.Sum(x => (x.SubTotalProducto)).ToString("###,###.#0"));
            totalCarrito = float.Parse(CarDB.Sum(x => x.TotalProducto).ToString("N2"));
            CountCart = CarDB.Count;

            App.Funciones.PopupCerrar();

            if (CountCart <= 0)
                IsVisibleImagenBuscar = true;
            else
                IsVisibleImagenBuscar = false;

            //Última Línea, no agregar nada después
            foreach (TblCarrito C in CarDB)
                productsList.Add(App.Funciones.ConvierteCarritoAArticulo(C));
        }

        /// <summary>
        /// Función que nos redirige a la pantalla de Búsqueda
        /// </summary>
        private async void AgregaArticulo()
        {
            TextoOverlay = "Abriendo búsqueda ...";
            App.Funciones.PopupEspera();
            await Shell.Current.GoToAsync("Iniciopage");
            App.Funciones.PopupCerrar();
        }

        /// <summary>
        /// Eliminar el Artículo indicado del Carrito
        /// </summary>
        /// <param name="obj">Artículo a Eliminar</param>
        private async void EliminarArtDelCarrito(Datos.Inicio.Request.Articulo obj)
        {
            try
            {
                List<TblCarrito> CarDB1 = App.ServiciosBD.ObtenerListaEntidadLocal(new TblCarrito().GetType().Name).OfType<TblCarrito>().ToList();
                if (obj.Articulo_Regalo.Trim().Length > 0 && CarDB1.Where(x => x.SkuArticulo == obj.Articulo_Regalo).ToList().Count > 0)
                {
                    if (obj.Piezas < obj.Promocion_Base)
                    {
                        if (await App.Current.MainPage.DisplayAlert("Aviso", "¿Desea eliminar el artículo del Carrito?", "Si", "No"))
                        {
                            TextoOverlay = "Eliminando ...";
                            App.Funciones.PopupEspera();
                            App.ServiciosBD.EliminarRegistroEntidadLocal(App.Funciones.ConvertirArticuloACarrito(obj, 0));
                        }
                        else
                            return;
                    }
                    else
                    {
                        if (await App.Current.MainPage.DisplayAlert("Aviso", "¿Desea eliminar el artículo del Carrito?, tambien se eliminará su pieza de regalo.", "Si", "No"))
                        {
                            TextoOverlay = "Eliminando ...";
                            App.Funciones.PopupEspera();
                            TblCarrito CarArtPromo = CarDB1.Where(x => x.SkuArticulo == obj.Articulo_Regalo).ToList()[0];
                            if (CarArtPromo.Piezas == 1)
                                App.ServiciosBD.EliminarRegistroEntidadLocal(CarArtPromo);
                            else
                                App.ServiciosBD.ActualizarRegistroEntidadLocal(App.Funciones.ConvertirArticuloACarrito(App.Funciones.ConvierteCarritoAArticulo(CarArtPromo), -1));

                            App.ServiciosBD.EliminarRegistroEntidadLocal(App.Funciones.ConvertirArticuloACarrito(obj, 0));                            
                        }
                        else
                            return;
                    }
                }
                else
                {
                    if (await App.Current.MainPage.DisplayAlert("Aviso", "¿Desea eliminar el artículo del Carrito?", "Si", "No"))
                    {
                        TextoOverlay = "Eliminando ...";
                        App.Funciones.PopupEspera();
                        App.ServiciosBD.EliminarRegistroEntidadLocal(App.Funciones.ConvertirArticuloACarrito(obj, 0));
                    }
                    else
                        return;
                }

                CountCart--;
                productsList.Clear();
                List<TblCarrito> CarDB = App.ServiciosBD.ObtenerListaEntidadLocal(new TblCarrito().GetType().Name).OfType<TblCarrito>().ToList();
                ActualizaTotalVenta(CarDB.Sum(x => x.TotalProducto).ToString("###,###.#0"), CarDB.Sum(x => x.Piezas), CarDB.Sum(x => (x.DescuentoMonto)).ToString("###,###.#0"), CarDB.Sum(x => (x.SubTotalProducto)).ToString("###,###.#0"));
                totalCarrito = float.Parse(CarDB.Sum(x => x.TotalProducto).ToString("N2"));
                App.Funciones.PopupCerrar();
                //Última Línea, no agregar nada después
                foreach (TblCarrito product in CarDB)
                {                    
                    productsList.Add(App.Funciones.ConvierteCarritoAArticulo(product));
                }
            }
            catch (Exception)
            {
               App.Funciones.PopupCerrar();
                var toast = Toast.Make("Error al eliminar el artículo del carrito.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
                return;
            }
        }

        /// <summary>
        /// Función para Cancelar la Venta y borrar los datos
        /// </summary>
        /// <param name="obj"></param>
        public async void CancelaVenta(Datos.Inicio.Request.Articulo obj)
        {
            List<TblCarrito> CarDB = App.ServiciosBD.ObtenerListaEntidadLocal(new TblCarrito().GetType().Name).OfType<TblCarrito>().ToList();
            if (CarDB.Count <= 0)
            {
                if (await App.Current.MainPage.DisplayAlert("Aviso", "¿Desea cancelar esta venta?", "Si", "No"))
                {
                    TextoOverlay = "Cancelando ...";
                    App.Funciones.PopupEspera();
                    App.ServiciosBD.LimpiarEntidadLocal(new TblCarrito());
                    App.ServiciosBD.LimpiarEntidadLocal(new TblGeneralesVenta());
                    App.ServiciosBD.LimpiarEntidadLocal(new TblPayment());
                    App.ServiciosBD.LimpiarEntidadLocal(new TblArticulosCupones());
                    //EFO
                    //await Shell.Current.GoToAsync("Iniciopage");                   

                    App.Funciones.PopupCerrar();
                    return;
                }
                else
                    return;
            }

            bool answer = await App.Current.MainPage.DisplayAlert("Aviso", "¿Desea eliminar TODOS los Productos del Carrito?", "Si", "No");
            if (answer == true)
            {
                //App.ServiciosBD.LimpiarEntidadLocal(new TblCarrito());
                //App.ServiciosBD.LimpiarEntidadLocal(new TblGeneralesVenta());
                //App.ServiciosBD.LimpiarEntidadLocal(new TblPayment());
                //App.ServiciosBD.LimpiarEntidadLocal(new TblArticulosCupones());
                //ActualizaTotalVenta("0.00", 0, "0.00", "0.00");
                //totalCarrito = float.Parse("0.00");
                //CountCart = 0;
                //productsList.Clear();
                //IsVisibleImagenBuscar = true;

                //EFO
                //await Shell.Current.GoToAsync("Iniciopage");
                //var tabbedPage = App.Current.MainPage as TabbedPage;
                //tabbedPage.CurrentPage = tabbedPage.Children[0];

                var tabbedPage = App.Current.MainPage;
                
                var pages = Shell.Current.CurrentPage;
                //this.tabControl.SelectedIndex = 1;


                var mainPage = App.Current.MainPage as TabbedPage;
                //mainPage.CurrentPage = mainPage.Children[0];

                var tabbedPage1 = App.Current.MainPage as AppShell;
            }
        }

        /// <summary>
        /// Aumenta el número de piezas al Artículo indicado
        /// </summary>
        /// <param name="obj">Artículo indicado</param>
        public async void AgregarMasPiezas(Datos.Inicio.Request.Articulo obj)
        {
            try
            {
                List<TblCarrito> CarDB = App.ServiciosBD.ObtenerListaEntidadLocal(new TblCarrito().GetType().Name).OfType<TblCarrito>().ToList();
                foreach (Datos.Inicio.Request.Articulo A in productsList)
                {
                    if (A.Sku == obj.Sku)
                        App.ServiciosBD.ActualizarRegistroEntidadLocal(App.Funciones.ConvertirArticuloACarrito(obj, 1));
                }

                CarDB.Clear();
                CarDB = App.ServiciosBD.ObtenerListaEntidadLocal(new TblCarrito().GetType().Name).OfType<TblCarrito>().ToList();
                TblCarrito Car2 = CarDB.Where(x => x.SkuArticulo == obj.Sku).ToList()[0];
                if (obj.Promocion_Base > 0 && obj.Articulo_Regalo.Trim().Length > 0)
                {
                    if ((Car2.Piezas % obj.Promocion_Base) == 0)
                    {
                        TextoOverlay = "Verificando promo ...";
                        App.Funciones.PopupEspera();
                        var ResBusqueda = await DStore.ObtenListaBusqueda(obj.Articulo_Regalo);
                        if (ResBusqueda[0].CantDisponible <= 0)
                        {
                           App.Funciones.PopupCerrar();
                            var toast = Toast.Make("El artículo de regalo no tiene existencias, no es posible agregarlo.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                            await toast.Show();
                        }
                        else
                        {
                            App.Funciones.PopupCerrar();
                            await App.Current.MainPage.DisplayAlert("Aviso", "Este artículo ha alcanzado " + Convert.ToInt32(obj.Promocion_Base) + " pieza(s) de la promoción, se agregará el artículo de regalo:\n\r\n\r" + ResBusqueda[0].Sku.Trim() + " - " + ResBusqueda[0].Nombre.Trim(), "Aceptar");
                            CarDB = App.ServiciosBD.ObtenerListaEntidadLocal(new TblCarrito().GetType().Name).OfType<TblCarrito>().ToList();
                            if (CarDB.Where(x => x.SkuArticulo.ToString() == ResBusqueda[0].Sku || x.Nombre == ResBusqueda[0].Nombre).ToList().Count > 0)
                                App.ServiciosBD.ActualizarRegistroEntidadLocal(App.Funciones.ConvertirArticuloACarrito(ResBusqueda[0], (Convert.ToInt32(Car2.Piezas) / Convert.ToInt32(obj.Promocion_Base))));
                            else
                                App.ServiciosBD.AgregarRegistroEntidadLocal(App.Funciones.ConvertirArticuloACarrito(ResBusqueda[0], (Convert.ToInt32(Car2.Piezas) / Convert.ToInt32(obj.Promocion_Base))));
                        }
                    }
                }

                CarDB.Clear();
                CarDB = App.ServiciosBD.ObtenerListaEntidadLocal(new TblCarrito().GetType().Name).OfType<TblCarrito>().ToList();
                productsList.Clear();
                ActualizaTotalVenta(CarDB.Sum(x => x.TotalProducto).ToString("###,###.#0"), CarDB.Sum(x => x.Piezas), CarDB.Sum(x => (x.DescuentoMonto)).ToString("###,###.#0"), CarDB.Sum(x => (x.SubTotalProducto)).ToString("###,###.#0"));
                totalCarrito = float.Parse(CarDB.Sum(x => x.TotalProducto).ToString("N2"));

                //Última Línea, no agregar nada después
                foreach (TblCarrito product in CarDB)
                {
                    productsList.Add(App.Funciones.ConvierteCarritoAArticulo(product));
                }

            }
            catch (Exception)
            { 
                App.Funciones.PopupCerrar();
                var toast = Toast.Make("Error al actualizar las piezas del artículo.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
                return;
            }
        }

        /// <summary>
        /// Disminuye el número de piezas al Artículo indicado
        /// </summary>
        /// <param name="obj">Artículo indicado</param>
        public async void QuitarMasPiezas(Datos.Inicio.Request.Articulo obj)
        {
            try
            {
                List<TblCarrito> CarDB = App.ServiciosBD.ObtenerListaEntidadLocal(new TblCarrito().GetType().Name).OfType<TblCarrito>().ToList();
                               
                if (CarDB.Where(x => x.SkuArticulo == obj.Sku).ToList()[0].Piezas <= 1)
                {
                    var toast = Toast.Make("Haz alcanzado el mínimo de piezas.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                    return;
                }
                

                foreach (Datos.Inicio.Request.Articulo A in productsList)
                {
                    if (A.Sku == obj.Sku)
                        App.ServiciosBD.ActualizarRegistroEntidadLocal(App.Funciones.ConvertirArticuloACarrito(obj, -1));
                }

                CarDB.Clear();
                CarDB = App.ServiciosBD.ObtenerListaEntidadLocal(new TblCarrito().GetType().Name).OfType<TblCarrito>().ToList();
                TblCarrito Car2 = CarDB.Where(x => x.SkuArticulo == obj.Sku).ToList()[0];
                if (obj.Promocion_Base > 0 && obj.Articulo_Regalo.Trim().Length > 0)
                {
                    if ((Car2.Piezas % obj.Promocion_Base) != 0)
                    {
                        TblCarrito Car3 = CarDB.Where(x => x.SkuArticulo == obj.Articulo_Regalo).ToList()[0];
                        if (Car3 != null)
                        {
                            if ((Car2.Piezas / obj.Promocion_Base) < Car3.Piezas)
                            {

                                if (CarDB.Where(x => x.SkuArticulo == obj.Articulo_Regalo).ToList().Count > 0)
                                {
                                    TblCarrito CarArtPromo = CarDB.Where(x => x.SkuArticulo == obj.Articulo_Regalo).ToList()[0];
                                    await App.Current.MainPage.DisplayAlert("Aviso", "Se eliminará el artículo de regalo: \r\n\r\n" + CarArtPromo.SkuArticulo + " - " + CarArtPromo.Nombre + "\r\n\r\nque estaba incluido en la promoción de este artículo:\n\r\n\r" + obj.Sku.Trim() + " - " + obj.Nombre.Trim() + "\n\r\n\r Para volver aplicar la promoción se deben adquirir " + obj.Promocion_Base + " piezas.", "Aceptar");
                                    if (CarArtPromo.Piezas == 1)
                                        App.ServiciosBD.EliminarRegistroEntidadLocal(CarArtPromo);
                                    else
                                        App.ServiciosBD.ActualizarRegistroEntidadLocal(App.Funciones.ConvertirArticuloACarrito(App.Funciones.ConvierteCarritoAArticulo(CarArtPromo), -1));
                                }
                            }
                        }
                    }
                }

                CarDB.Clear();
                CarDB = App.ServiciosBD.ObtenerListaEntidadLocal(new TblCarrito().GetType().Name).OfType<TblCarrito>().ToList();
                productsList.Clear();
                ActualizaTotalVenta(CarDB.Sum(x => x.TotalProducto).ToString("###,###.#0"), CarDB.Sum(x => x.Piezas), CarDB.Sum(x => (x.DescuentoMonto)).ToString("###,###.#0"), CarDB.Sum(x => (x.SubTotalProducto)).ToString("###,###.#0"));
                totalCarrito = float.Parse(CarDB.Sum(x => x.TotalProducto).ToString("N2"));

                //Última Línea, no agregar nada después
                foreach (TblCarrito product in CarDB)
                {
                    productsList.Add(App.Funciones.ConvierteCarritoAArticulo(product));
                }
            }
            catch (Exception)
            {
                var toast = Toast.Make("Error al actualizar las piezas del artículo.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
                return;
            }
        }

        public async void PiezaManual(Datos.Inicio.Request.Articulo obj)
        {
            string Piezas = "";
            Piezas = await App.Current.MainPage.DisplayPromptAsync("Ingreso de Piezas", "Ingresa el número de piezas para el artículo.", "Aceptar", "Cancelar");
            if (Piezas != null)
            {
                if (!App.Funciones.IsNumeric(Piezas) || Piezas == "")
                {
                    var toast = Toast.Make("Debes ingresar una cantidad.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                    return;
                }

                if (Convert.ToInt32(Piezas) <= 0)
                {
                    var toast = Toast.Make("La cantidad de piezas debe ser mayor a 0.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                    return;
                }

                List<Articulo> LstArts = new();
                PzsManuales = true;
                List<TblCarrito> CarDB = App.ServiciosBD.ObtenerListaEntidadLocal(new TblCarrito().GetType().Name).OfType<TblCarrito>().ToList();
                TblCarrito Car2 = CarDB.Where(x => x.SkuArticulo == obj.Sku).ToList()[0];
                App.ServiciosBD.ActualizarRegistroEntidadLocal(App.Funciones.ConvertirArticuloACarrito(App.Funciones.ConvierteCarritoAArticulo(Car2), Convert.ToInt32(Piezas)));
                
                if (obj.Promocion_Base > 0 && obj.Articulo_Regalo.Trim().Length > 0)
                {
                    if (Convert.ToInt32(Piezas) >= obj.Promocion_Base)
                    {
                        TextoOverlay = "Verificando promo ...";
                        App.Funciones.PopupEspera();
                        var ResBusqueda = await DStore.ObtenListaBusqueda(obj.Articulo_Regalo);
                        if (ResBusqueda[0].CantDisponible <= 0)
                        {
                            App.Funciones.PopupCerrar();
                            var toast = Toast.Make("El artículo de regalo no tiene existencias, no es posible agregarlo.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                            await toast.Show();
                        }
                        else
                        {
                            LstArts = ResBusqueda;
                            App.Funciones.PopupCerrar();
                            await App.Current.MainPage.DisplayAlert("Aviso", "Este artículo ha alcanzado " + Convert.ToInt32(obj.Promocion_Base) + " pieza(s) de la promoción, se agregará el artículo de regalo:\n\r\n\r" + ResBusqueda[0].Sku.Trim() + " - " + ResBusqueda[0].Nombre.Trim(), "Aceptar");
                            CarDB = App.ServiciosBD.ObtenerListaEntidadLocal(new TblCarrito().GetType().Name).OfType<TblCarrito>().ToList();
                            if (CarDB.Where(x => x.SkuArticulo.ToString() == ResBusqueda[0].Sku || x.Nombre == ResBusqueda[0].Nombre).ToList().Count > 0)
                                App.ServiciosBD.ActualizarRegistroEntidadLocal(App.Funciones.ConvertirArticuloACarrito(ResBusqueda[0], (Convert.ToInt32(Piezas) / Convert.ToInt32(obj.Promocion_Base))));
                            else
                                App.ServiciosBD.AgregarRegistroEntidadLocal(App.Funciones.ConvertirArticuloACarrito(ResBusqueda[0], (Convert.ToInt32(Piezas) / Convert.ToInt32(obj.Promocion_Base))));
                        }
                    }
                    else
                    {
                        TblCarrito Car3 = CarDB.Where(x => x.SkuArticulo == obj.Articulo_Regalo).ToList()[0];
                        if (Car3 != null)
                        {
                            await App.Current.MainPage.DisplayAlert("Aviso", "Se eliminará el artículo de regalo: \r\n\r\n" + Car3.SkuArticulo + " - " + Car3.Nombre.Trim() + "\r\n\r\nque estaba incluido en la promoción de este artículo:\n\r\n\r" + obj.Sku.Trim() + " - " + obj.Nombre.Trim() + "\n\r\n\r Para volver aplicar la promoción se deben adquirir " + obj.Promocion_Base + " piezas.", "Aceptar");
                            App.ServiciosBD.EliminarRegistroEntidadLocal(Car3);
                        }
                    }
                }

                productsList.Clear();
                CarDB.Clear();
                CarDB = App.ServiciosBD.ObtenerListaEntidadLocal(new TblCarrito().GetType().Name).OfType<TblCarrito>().ToList();
                ActualizaTotalVenta(CarDB.Sum(x => x.TotalProducto).ToString("###,###.#0"), CarDB.Sum(x => x.Piezas), CarDB.Sum(x => (x.TotalDescuento)).ToString("###,###.#0"), CarDB.Sum(x => (x.SubTotalProducto)).ToString("###,###.#0"));
                PzsManuales = false;
                foreach (TblCarrito product in CarDB)
                {
                    if (product.PrecioUnitario < product.Costo_Unitario)
                        product.IsVisibleLowCostGif = true;
                    productsList.Add(App.Funciones.ConvierteCarritoAArticulo(product));
                }
            }
        }

        /// <summary>
        /// Actualiza el Monto Total de la Venta
        /// </summary>
        /// <param name="Total">Monto Final a pagar</param>
        /// <param name="Piezas">Número de Piezas en el Carrito</param>
        /// <param name="Ahorrado">Monto Ahorrado</param>
        /// <param name="SubTotal">Monto Total del Carrito</param>
        private void ActualizaTotalVenta(string Total, int Piezas, string Ahorrado, string SubTotal)
        {
            TotalVenta.Clear();
            TotalVenta.Add(new TotalVenta(Total, Piezas, Ahorrado, SubTotal));
        }

        /// <summary>
        /// Muestra la pantalla de Pagos
        /// </summary>
        private async void PagarCarrito()
        {
            List<TblGeneralesVenta> datosGrales = App.ServiciosBD.ObtenerListaEntidadLocal("TblGeneralesVenta").OfType<TblGeneralesVenta>().ToList();
            if (datosGrales.Count <= 0)
            {
                PopupGeneralesVenta = new();
                Application.Current.MainPage.ShowPopup(PopupGeneralesVenta);
                return;
            }            

            try
            {
                string Articulo = "";

                List<TblCarrito> CarDB = App.ServiciosBD.ObtenerListaEntidadLocal(new TblCarrito().GetType().Name).OfType<TblCarrito>().ToList();
                if (CarDB.Count <= 0)
                {
                    var toast = Toast.Make("No hay artículos en el carrito, agrega artículos para poder ingresar a la pantalla de pago.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                    return;
                }
                else
                {
                    TextoOverlay = "Procesando ...";
                    App.Funciones.PopupEspera();

                    decimal Monto = 0;
                    List<VentaDetalle> lista_Detalle = new List<VentaDetalle>();
                    foreach (TblCarrito producto in CarDB)
                    {
                        VentaDetalle vDetalle = new VentaDetalle(
                            producto.SkuArticulo,
                            producto.Piezas,
                            float.Parse(producto.PrecioUnitario.ToString("N2")),
                            producto.DescuentoMonto,
                            producto.DescuentoPorcentaje,
                            producto.Lote_Caducidad,
                            producto.TotalProducto,
                            producto.IvaMonto,
                            producto.IvaPorcentaje,
                            producto.IepsMonto,
                            producto.IepsPorcentaje,
                            0);
                        lista_Detalle.Add(vDetalle);
                    }

                    ApiDataStore DStore = new();
                    var jsonDetalle = Newtonsoft.Json.JsonConvert.SerializeObject(lista_Detalle);
                    var ResPromociones = await DStore.CalculaPromociones(jsonDetalle);
                    ResPromocionesVentas ResPromo = Newtonsoft.Json.JsonConvert.DeserializeObject<ResPromocionesVentas>(ResPromociones);
                    System.String[] Detalle = ResPromo.Data.Split(",");
                    App.ServiciosBD.LimpiarEntidadLocal(new TblArticulosCupones());                    
                    for (int i = 0; i <= Detalle.Length - 1; i++)
                    {
                        Detalle[i] = Detalle[i].Replace("{", "").Replace("}", "").Replace("[", "").Replace("]", "").Replace("\"", "").Replace("Table:", "");
                        if (Detalle[i].IndexOf("Articulo_Id") >= 0)
                            Articulo = Detalle[i].Substring(Detalle[i].IndexOf(":") + 1);
                        else if (Detalle[i].IndexOf("Cupon_Monto") >= 0)
                        {
                            Monto = Convert.ToDecimal(Detalle[i].Substring(Detalle[i].IndexOf(":") + 1));
                            List<TblArticulosCupones> ArtsCup = App.ServiciosBD.ObtenerListaEntidadLocal(new TblArticulosCupones().GetType().Name).OfType<TblArticulosCupones>().ToList();
                            if (ArtsCup.Where(x => x.Articulo == Articulo).ToList().Count <= 0)
                            {
                                App.ServiciosBD.AgregarRegistroEntidadLocal(new TblArticulosCupones { Articulo = Articulo, Monto = Monto });
                                Articulo = "";
                                Monto = 0;
                            }
                            else
                            {
                                TblArticulosCupones AxC = ArtsCup.Where(x => x.Articulo == Articulo).ToList()[0];
                                AxC.Monto = AxC.Monto + Monto;
                                App.ServiciosBD.ActualizarRegistroEntidadLocal(AxC);
                                Articulo = "";
                                Monto = 0;
                            }
                        }
                    }

                    List<TblArticulosCupones> Cupones = App.ServiciosBD.ObtenerListaEntidadLocal(new TblArticulosCupones().GetType().Name).OfType<TblArticulosCupones>().ToList();

                    string SubTotal = App.Funciones.BuscaValorArray(Detalle, "Subtotal");
                    string Promociones = Cupones.Sum(x => x.Monto).ToString("###,##0.00");

                    string IVA = App.Funciones.BuscaValorArray(Detalle, "IVA");
                    string IEPS = App.Funciones.BuscaValorArray(Detalle, "IEPS");
                    string Descuentos = App.Funciones.BuscaValorArray(Detalle, "DescuentoMonto");
                    SubTotal = (Convert.ToDecimal(SubTotal) + Convert.ToDecimal(IVA)).ToString("N2");
                    string TotalPagar = (Convert.ToDecimal(SubTotal) - Convert.ToDecimal(Descuentos) - Convert.ToDecimal(Promociones)).ToString("N2");

                    int tipoDoc = App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblGeneralesVenta", App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblConfiguracion", "CLIENTE_VIRTUAL", "Valor"), "IdTipoDocumento") == null ? 0 : int.Parse(App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblGeneralesVenta", App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblConfiguracion", "CLIENTE_VIRTUAL", "Valor"), "IdTipoDocumento")); 
                    //1:Contado   10:Click_Collect   14:Domicilio
                    if (tipoDoc == 1 || tipoDoc == 10 || tipoDoc == 14)
                        await Shell.Current.GoToAsync($"Payment?subtotal={SubTotal}&promociones={Promociones}&iva={IVA}&ieps={IEPS}&descuentos={Descuentos}&total={TotalPagar}");
                    else if (tipoDoc == 4) //Crédito
                        App.Funciones.crear_Venta(false, "", TotalPagar, "", "", "", "", Promociones);

                    App.Funciones.PopupCerrar();
                }
            }
            catch (Exception ex)
            {
                App.Funciones.PopupCerrar();
                var toast = Toast.Make("Error en el cálculo del resumen de venta. No hay datos.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
        }
         
        /// <summary>
        /// Se busca el Número del Monedero con el dato indicado en el Filtro
        /// </summary>
        private async void btn_BuscarMonederoPopUp()
        {
            string filtro = await App.Current.MainPage.DisplayPromptAsync("Búsqueda de Monedero", "Indica el dato por el cual se buscará el Monedero. Por ejemplo: Nombre, Tarjeta o Teléfono", "Aceptar", "Cancelar", "Dato de Búsqueda", 50, keyboard: Keyboard.Text, "");

            try
            {
                if (filtro != null && filtro != "")
                {
                    TextoOverlay = "Buscando ...";
                    App.Funciones.PopupEspera();

                    ApiDataStore DStore = new();
                    var ResBusqueda = await DStore.Consulta_SocioFrecuente(int.Parse(App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblConfiguracion", "EMPRESA_VIRTUAL", "Valor")), int.Parse(App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblConfiguracion", "SUCURSAL_VIRTUAL", "Valor")), int.Parse(App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblConfiguracion", "CAJA_VIRTUAL", "Valor")), filtro);

                    if (ResBusqueda != null)
                    {
                        monederoList.Clear();
                        foreach (var member in ResBusqueda.DataL)
                        {
                            Datos.Carrito.Response.ConsultaSocioFrecuenteDetalle Info = new(member.Correo.ToString(), member.Nombre.ToString(), int.Parse(member.Socio.ToString()), member.Tarjeta.ToString(), member.Telefono.ToString());
                            monederoList.Add(Info);
                        }
                        App.Funciones.PopupCerrar();
                    }
                    else
                    {
                        App.Funciones.PopupCerrar();
                        var toast = Toast.Make("No se encontró un Monedero con el dato indicado.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                        await toast.Show();
                        return;
                    }
                }
            }
            catch (Exception)
            {
                App.Funciones.PopupCerrar();
                var toast = Toast.Make("Error al obtener la información del Monedero.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
                return;
            }
        }

        /// <summary>
        /// Se guardan los datos ingresados en la Ventana PopUp
        /// </summary>
        private async void btn_DatosGeneralesAceptar()
        {
            if (IsCheckDomicilio == true || IsCheckTienda == true)
            {
                string Suc_CCId = "";
                string Suc_CCNombre = "";

                if (IsCheckTienda == true && SucClickCollect_IdSelected != null)
                {
                    Suc_CCId = SucClickCollect_IdSelected.Id;
                    Suc_CCNombre = SucClickCollect_IdSelected.Nombre;
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Aviso", "Necesitas seleccionar una Sucursal", "OK");
                    return;
                }
                bool answer = await App.Current.MainPage.DisplayAlert("Aviso", "¿Seguro que desea guardar los datos?", "Si", "No");
                if (answer == true)
                {
                    datosGenerales = new();
                    datosGenerales.IdUsuario = int.Parse(App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblConfiguracion", "CLIENTE_VIRTUAL", "Valor"));
                    datosGenerales.IdTipoDocumento = IsCheckDomicilio == true ? "14" : "10";
                    datosGenerales.NombreDocumento = IsCheckDomicilio == true ? "Entrega a Domicilio" : "Click_Collect";
                    datosGenerales.IdTipoCliente = "0";
                    datosGenerales.NombreCliente = App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblGeneralesVenta", App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblConfiguracion", "CLIENTE_VIRTUAL", "Valor"), "NombreCliente");
                    datosGenerales.NumMonedero = NumMonederoLealtadPopUp == null ? "" : NumMonederoLealtadPopUp;
                    datosGenerales.SaldoMonedero = SaldoMonedero;
                    datosGenerales.IdSocioMonedero = 0;
                    datosGenerales.PTarjeta = "";
                    datosGenerales.Transaccion = "";
                    datosGenerales.Direccion = "";
                    datosGenerales.SucursalClickCollect_Id = Suc_CCId;
                    datosGenerales.SucursalClickCollect_Nombre = Suc_CCNombre;
                    
                    List<TblGeneralesVenta> datos = App.ServiciosBD.ObtenerListaEntidadLocal("TblGeneralesVenta").OfType<TblGeneralesVenta>().ToList();
                    if (datos.Count <= 0)
                        App.ServiciosBD.AgregarRegistroEntidadLocal(datosGenerales);
                    else
                        App.ServiciosBD.ActualizarRegistroEntidadLocal(datosGenerales);

                    DGV_Documento = App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblGeneralesVenta", datosGenerales.IdUsuario.ToString(), "NombreDocumento") == null ? "" : App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblGeneralesVenta", datosGenerales.IdUsuario.ToString(), "NombreDocumento");
                    DGV_Monedero = App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblGeneralesVenta", datosGenerales.IdUsuario.ToString(), "NumMonedero") == "" ? "SIN MONEDERO" : App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblGeneralesVenta", datosGenerales.IdUsuario.ToString(), "NumMonedero");
                    PopupGeneralesVenta.Close();

                    //mandar a Payment
                    PagarCarrito();
                }
            }
            else
                await App.Current.MainPage.DisplayAlert("Aviso", "Necesitas seleccionar un Tipo de Entrega", "OK");
        }

        /// <summary>
        /// No se agrega el Número del Monedero de la Ventana PopUp a la Ventana de Pagos
        /// </summary>
        private async void btn_DatosGeneralesCancelar()
        {
            bool answer = await App.Current.MainPage.DisplayAlert("Aviso", "¿Seguro que desea salir sin guardar los datos?", "Si", "No");
            if (answer == true)
            {
                if (PopupGeneralesVenta != null)
                {                   
                    PopupGeneralesVenta.CerrarPopup();
                    PopupGeneralesVenta = null;
                }
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
                        List<TblGeneralesVenta> datos = App.ServiciosBD.ObtenerListaEntidadLocal("TblGeneralesVenta").OfType<TblGeneralesVenta>().ToList(); ;
                        TblGeneralesVenta datosGenerales = new();
                        datosGenerales.IdUsuario = int.Parse(App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblConfiguracion", "CLIENTE_VIRTUAL", "Valor"));
                        datosGenerales.IdTipoDocumento = datos[0].IdTipoDocumento;
                        datosGenerales.NombreDocumento = datos[0].NombreDocumento;
                        datosGenerales.IdTipoCliente = datos[0].IdTipoCliente;
                        datosGenerales.NombreCliente = datos[0].NombreCliente;
                        datosGenerales.NumMonedero = obj.Tarjeta;
                        datosGenerales.SaldoMonedero = SaldoMonedero;
                        datosGenerales.PTarjeta = datos[0].PTarjeta;
                        datosGenerales.Transaccion = datos[0].Transaccion;
                        App.ServiciosBD.ActualizarRegistroEntidadLocal(datosGenerales);
                        DGV_Monedero = App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblGeneralesVenta", App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblConfiguracion", "CLIENTE_VIRTUAL", "Valor"), "NumMonedero") == null ? "SIN MONEDERO" : App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblGeneralesVenta", App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblConfiguracion", "CLIENTE_VIRTUAL", "Valor"), "Nummonedero");
                        List<TblGeneralesVenta> datos2 = App.ServiciosBD.ObtenerListaEntidadLocal("TblGeneralesVenta").OfType<TblGeneralesVenta>().ToList(); ;
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
        /// Se muestra el PopUp de Monedero
        /// </summary>
        private void Popup_Monedero_Abrir()
        {
            PopupMonedero = new();
            Application.Current.MainPage.ShowPopup(PopupMonedero);
        }

        /// <summary>
        /// Se muestra el PopUp de Búsqueda Monedero
        /// </summary>
        private void Popup_BusquedaMonedero_Abrir()
        {            
            PopupBusquedaMonedero = new();
            Application.Current.MainPage.ShowPopup(PopupBusquedaMonedero);
        }

        /// <summary>
        /// Se cierra el PopUp de Búsqueda Monedero
        /// </summary>
        private void Popup_BusquedaMonedero_Cerrar()
        {
            monederoList.Clear();
            PopupBusquedaMonedero.CerrarPopup();
            PopupBusquedaMonedero = null;
        }

        /// <summary>
        /// Se cierra el PopUp de Monedero
        /// </summary>
        private void Popup_Monedero_Cerrar()
        {
            monederoList.Clear();
            PopupMonedero.CerrarPopup();
            PopupMonedero = null;
        }

        /// <summary>
        /// Función para modificar el color del botón de Entrega a Domicilio
        /// </summary>
        private async void IsCheckedEntregaDomicilio()
        {
            IsCheckDomicilio = true;
            IsCheckTienda = false;
            ColorEntregaDomicilio = App.Funciones.GetColorResourceValue("ColorCarritoOpcionActiva");
            ColorEntregaTienda = App.Funciones.GetColorResourceValue("ColorCarritoOpcionInactiva");
            DomicilioVisible = true;
            SucursalVisible = false;

            //Mostrar direcciones guardadas
        }

        /// <summary>
        /// Función para modificar el color del botón de Recoger en Tienda
        /// </summary>
        private async void IsCheckedEntregaTienda()
        {
            IsCheckDomicilio = false;
            IsCheckTienda = true;
            ColorEntregaDomicilio = App.Funciones.GetColorResourceValue("ColorCarritoOpcionInactiva");
            ColorEntregaTienda = App.Funciones.GetColorResourceValue("ColorCarritoOpcionActiva");
            DomicilioVisible = false;
            SucursalVisible = true;

            //mostrar sucursales 
            ObtenerSucursalClickCollect().GetAwaiter();
        }

        /// <summary>
        /// Obtiene la lista de los Afiliados al Cliente seleccionado
        /// </summary>
        /// <returns></returns>
        public async Task ObtenerSucursalClickCollect()
        {
            try
            {                
                ApiDataStore Api = new();
                var rq = await Api.SucursalUbicacion_ClickCollect(int.Parse("3100")); //CP  EFO
                var ob = JsonConvert.DeserializeObject<List<TblSucursalClickCollect>>(rq);
                List<TblSucursalClickCollect> sucursalOrdenada = ob.OrderBy(c => c.Nombre).ToList();

                SucClickCollectList = sucursalOrdenada;

            }
            catch (Exception)
            {
                var toast = Toast.Make("Error al obtener la Lista de las Sucursales para Click_Collect.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
                return;
            }
        }
    }
}
