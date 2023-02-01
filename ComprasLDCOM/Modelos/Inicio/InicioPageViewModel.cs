using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using ComprasLDCOM.API;
using ComprasLDCOM.Datos.Configuraciones.BaseDeDatos;
using ComprasLDCOM.Datos.Inicio.Request;
using ComprasLDCOM.Popups.General;
using ComprasLDCOM.Popups.Inicio;
using ComprasLDCOM.Datos.Inicio.BaseDeDatos;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ComprasLDCOM.Modelos.Carrito;
using static SQLite.SQLite3;
using ComprasLDCOM.Datos.Cuenta.Response;

namespace ComprasLDCOM.Modelos.Inicio
{
    internal class InicioPageViewModel : BindableObject, INotifyPropertyChanged
    {
        private string _imagen = "";
        private ApiDataStore DStore = new ApiDataStore();
        private PopupPageEspera popEspera = null;
        private IDispatcherTimer timer;
        private PopupPageCodigoPostal popCP = new();

        public event PropertyChangedEventHandler PropertyChanged;

        protected override void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// Lista de tipo observable, la cual sera el binding de articulos con la vista
        /// </summary>
        public ObservableCollection<Articulo> _ArticulosBusqueda { get; set; } = new();

        public ObservableCollection<Articulo> ArticulosBusqueda
        {
            get { return _ArticulosBusqueda; }
            set
            {
                _ArticulosBusqueda = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ArticulosBusqueda)));
            }
        }

        /// <summary>
        /// Lista de tipo observable, la cual sera el binding de los estados con la vista
        /// </summary>
        public ObservableCollection<Estado> _EstadosRepublica { get; set; } = new();

        public ObservableCollection<Estado> EstadosRepublica
        {
            get { return _EstadosRepublica; }
            set
            {
                _EstadosRepublica = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EstadosRepublica)));
            }
        }

        /// <summary>
        /// Lista de tipo observable, la cual sera el binding de los municipios con la vista
        /// </summary>
        public ObservableCollection<Municipio> _MunicipiosRepublica { get; set; } = new();

        public ObservableCollection<Municipio> MunicipiosRepublica
        {
            get { return _MunicipiosRepublica; }
            set
            {
                _MunicipiosRepublica = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MunicipiosRepublica)));
            }
        }

        /// <summary>
        /// Lista de tipo observable, la cual sera el binding de las colonias con la vista
        /// </summary>
        public ObservableCollection<Colonia> _ColoniasRepublica { get; set; } = new();

        public ObservableCollection<Colonia> ColoniasRepublica
        {
            get { return _ColoniasRepublica; }
            set
            {
                _ColoniasRepublica = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ColoniasRepublica)));
            }
        }

        /// <summary>
        /// Imagen de tipo string donde se almacenara el base64
        /// </summary>
        public string Imagen
        {
            get => _imagen;
            set
            {
                if (_imagen != value)
                {
                    _imagen = value;
                    ImageSource img = App.Funciones.Base64aImagen(value);
                    ArtImagen64 = img;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Iamgen de tipo imagesource donde se almacenara la imagen
        /// </summary>
        public ImageSource ArtImagen64
        {
            get => (ImageSource)GetValue(ArtImagen64Property);
            set
            {
                SetValue(ArtImagen64Property, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// propiedad para limpiar la caja de busqueda
        /// </summary>
        public string LimpiaTexto
        {
            get => (string)GetValue(LimpiaTextoProperty);
            set
            {
                SetValue(LimpiaTextoProperty, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Texto a mostrar en el PopUp de espera
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
        /// Para la visibilidad de la captura del codigo postal
        /// </summary>
        public bool IsVisibleInicioPopupCodigoPostal
        {
            get => (bool)GetValue(IsVisibleInicioPopupCodigoPostalProperty);
            set
            {
                SetValue(IsVisibleInicioPopupCodigoPostalProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsVisibleInicioPopupCodigoPostal)));
            }
        }

        /// <summary>
        /// Para la visibilidad de la captura del codigo postal x colonia
        /// </summary>
        public bool IsVisibleInicioPopupColonia
        {
            get => (bool)GetValue(IsVisibleInicioPopupColoniaProperty);
            set
            {
                SetValue(IsVisibleInicioPopupColoniaProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsVisibleInicioPopupColonia)));
            }
        }

        /// <summary>
        /// Color del check de codigo postal
        /// </summary>
        public Color ColorFondoCodigoPostal
        {
            get => (Color)GetValue(ColorFondoCodigoPostalProperty);
            set
            {
                SetValue(ColorFondoCodigoPostalProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ColorFondoCodigoPostal)));
            }
        }

        /// <summary>
        /// Color del texto del check de codigo postal
        /// </summary>
        public Color ColorTextoCodigoPostal
        {
            get => (Color)GetValue(ColorTextoCodigoPostalProperty);
            set
            {
                SetValue(ColorTextoCodigoPostalProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ColorTextoCodigoPostal)));
            }
        }

        /// <summary>
        /// Color del check de colonia
        /// </summary>
        public Color ColorFondoColonia
        {
            get => (Color)GetValue(ColorFondoColoniaProperty);
            set
            {
                SetValue(ColorFondoColoniaProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ColorFondoColonia)));
            }
        }

        /// <summary>
        /// Color del texto del check de la colonia
        /// </summary>
        public Color ColorTextoColonia
        {
            get => (Color)GetValue(ColorTextoColoniaProperty);
            set
            {
                SetValue(ColorTextoColoniaProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ColorTextoColonia)));
            }
        }

        /// <summary>
        /// Texto a mostrar o a obtener del codigo postal
        /// </summary>
        public string TextoCP
        {
            get => (string)GetValue(TextoCPProperty);
            set
            {
                SetValue(TextoCPProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextoCP)));
            }
        }

        /// <summary>
        /// Propiedad de tipo Estado para recibir el estado seleccionado
        /// </summary>
        private Estado EstadoId { get; set; }
        public Estado EstadoSel
        {
            get { return EstadoId; }
            set
            {
                if (EstadoSel != value)
                {
                    EstadoId = value;
                    CargarMunicipios(value).GetAwaiter();
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Propiedad de tipo Municipio para recibir el municipio seleccionado
        /// </summary>
        private Municipio MunicipioId { get; set; }
        public Municipio MunicipioSel
        {
            get { return MunicipioId; }
            set
            {
                if (MunicipioSel != value)
                {
                    MunicipioId = value;
                    CargarColonias(EstadoSel, value).GetAwaiter();
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Propiedad de tipo Colonia para recibir la colonia seleccionada
        /// </summary>
        private Colonia ColoniaId { get; set; }
        public Colonia ColoniaSel
        {
            get { return ColoniaId; }
            set
            {
                if (ColoniaSel != value)
                {
                    ColoniaId = value;
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

        /// <summary>
        /// Para la visibilidad del collectionview de busqueda
        /// </summary>
        public bool IsVisibleCollectionViewBusqueda
        {
            get => (bool)GetValue(IsVisibleCollectionViewBusquedaProperty);
            set
            {
                SetValue(IsVisibleCollectionViewBusquedaProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsVisibleCollectionViewBusqueda)));
            }
        }

        /// <summary>
        /// Comando para agregar a favoritos el articulo
        /// </summary>
        public ICommand AgregaFavoritos { get; }

        /// <summary>
        /// Comando para agregar al carrito el articulo seleccionado
        /// </summary>
        public ICommand AgregaCarrito { get; }

        /// <summary>
        /// Comando para mostrar el detalle del artículo seleccionado
        /// </summary>
        public ICommand MostrarInformacion { get; }

        /// <summary>
        /// Comando para la busqueda de los articulos
        /// </summary>
        public ICommand BusquedaDeArticulo { get; }

        /// <summary>
        /// Comando para mostrar el frame de codigo postal
        /// </summary>
        public ICommand IsCheckedCPxCodigoPostal { get; }

        /// <summary>
        /// Comando para mostrar el frame de la colonia
        /// </summary>
        public ICommand IsCheckedCPxColonia { get; }

        /// <summary>
        /// Comando para ejecuitar la busqueda de los datos virtuales
        /// </summary>
        public ICommand ObtenerDatosVirtuales { get; }


        /// <summary>
        /// Propiedades bindables
        /// </summary>
        public BindableProperty ArtImagen64Property = BindableProperty.Create(nameof(ArtImagen64), typeof(ImageSource), typeof(InicioPageViewModel));
        public BindableProperty LimpiaTextoProperty = BindableProperty.Create(nameof(LimpiaTexto), typeof(string), typeof(InicioPageViewModel));
        public BindableProperty TextoOverlayProperty = BindableProperty.Create(nameof(TextoOverlay), typeof(string), typeof(InicioPageViewModel));
        public BindableProperty IsVisibleInicioPopupCodigoPostalProperty = BindableProperty.Create(nameof(IsVisibleInicioPopupCodigoPostal), typeof(bool), typeof(InicioPageViewModel));
        public BindableProperty IsVisibleInicioPopupColoniaProperty = BindableProperty.Create(nameof(IsVisibleInicioPopupColonia), typeof(bool), typeof(InicioPageViewModel));
        public BindableProperty ColorFondoCodigoPostalProperty = BindableProperty.Create(nameof(ColorFondoCodigoPostal), typeof(Color), typeof(InicioPageViewModel));
        public BindableProperty ColorTextoCodigoPostalProperty = BindableProperty.Create(nameof(ColorTextoCodigoPostal), typeof(Color), typeof(InicioPageViewModel));
        public BindableProperty ColorFondoColoniaProperty = BindableProperty.Create(nameof(ColorFondoColonia), typeof(Color), typeof(InicioPageViewModel));
        public BindableProperty ColorTextoColoniaProperty = BindableProperty.Create(nameof(ColorTextoColonia), typeof(Color), typeof(InicioPageViewModel));
        public BindableProperty TextoCPProperty = BindableProperty.Create(nameof(TextoCP), typeof(string), typeof(InicioPageViewModel));
        public BindableProperty IsVisibleImagenBuscarProperty = BindableProperty.Create(nameof(IsVisibleImagenBuscar), typeof(bool), typeof(InicioPageViewModel));
        public BindableProperty IsVisibleCollectionViewBusquedaProperty = BindableProperty.Create(nameof(IsVisibleCollectionViewBusqueda), typeof(bool), typeof(InicioPageViewModel));

        public InicioPageViewModel()
        {
            IsVisibleInicioPopupCodigoPostal = false;
            IsVisibleImagenBuscar = true;
            IsVisibleCollectionViewBusqueda = false;
            ColorFondoCodigoPostal = App.Funciones.GetColorResourceValue("ColorPopupFondoInicativoBotonCPyColonia");
            ColorTextoCodigoPostal = App.Funciones.GetColorResourceValue("ColorPopupTextoInicativoBotonCPyColonia");
            ColorFondoColonia = App.Funciones.GetColorResourceValue("ColorPopupFondoInicativoBotonCPyColonia");
            ColorTextoColonia = App.Funciones.GetColorResourceValue("ColorPopupTextoInicativoBotonCPyColonia");

            Imagen = App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblConfiguracion", "LOGO_APP", "Valor");
            BusquedaDeArticulo = new Command<string>(BuscarArticulo);
            AgregaFavoritos = new Command<Articulo>(AgregarFavoritos);
            AgregaCarrito = new Command<Articulo>(AgregarCarrito);
            MostrarInformacion = new Command<Articulo>(MuestraDetalleArticulo);
            IsCheckedCPxCodigoPostal = new Command(MuestraInicioPopupCodigoPostal);
            IsCheckedCPxColonia = new Command(MuestraInicioPopupColonia);
            ObtenerDatosVirtuales = new Command<string>(ObtenDatosvirtuales);

            timer = Dispatcher.CreateTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += (s, e) =>
            {
                SeleccionCodigoPostal();
            };
            timer.Start();

            TextoCP = "";
            popEspera = null;
        }

        /// <summary>
        /// Busca el articulo ingresado en el control de busqueda de la vista
        /// </summary>
        /// <param name="ArticuloId">Código ó descripción del articulo a buscar</param>
        private async void BuscarArticulo(string ArticuloId)
        {
            try
            {
                if (ArticuloId == "" || ArticuloId == null)
                {
                    IsVisibleImagenBuscar = true;
                    IsVisibleCollectionViewBusqueda = false;
                    LimpiaTexto = "";
                    ArticulosBusqueda.Clear();
                    return;
                }

                IsVisibleImagenBuscar = false;
                IsVisibleCollectionViewBusqueda = true;
                TextoOverlay = "Buscando...";
                App.Funciones.PopupEspera();
                List<Articulo> ListaBusqueda = await DStore.ObtenListaBusqueda(ArticuloId);
                foreach (Articulo Art in ListaBusqueda)
                    ArticulosBusqueda.Add(Art);

                App.Funciones.PopupCerrar();
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                await App.Current.MainPage.DisplayAlert("Hola", "Tuvimos un problema para buscar el artículo que ingresaste, po favor intenta mas tarde.", "Aceptar");
            }
        }

        /// <summary>
        /// Metodo para mostrar el popup de la solicitud de codigo postal
        /// </summary>
        private void SeleccionCodigoPostal()
        {
            string Empresa = App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblConfiguracion", "EMPRESA_VIRTUAL", "Valor");
            string Sucursal = App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblConfiguracion", "SUCURSAL_VIRTUAL", "Valor");
            string Caja = App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblConfiguracion", "CAJA_VIRTUAL", "Valor");
            string Cliente = App.ServiciosBD.ObtenerDatoRegistroEntidadLocal("TblConfiguracion", "CLIENTE_VIRTUAL", "Valor");

            if (Empresa == "" || Sucursal == "" || Caja == "" || Cliente == "")
            {
                CargarEstados();
                App.Current.MainPage.ShowPopup(popCP);
            }

            timer.Stop();
        }

        /// <summary>
        /// Método para agregar el artículo seleccionado a favoritos
        /// </summary>
        /// <param name="Art">Objeto de tipo Artículo - Contiene la información del artículo seleccionado</param>
        private async void AgregarFavoritos(Articulo Art)
        {
            try
            {
                if (App.ServiciosBD.ObtenerRegistroEntidadLocal("TblFavoritos", Art.Sku).Count > 0)
                {
                    var toastF = Toast.Make("Artículo ya está en favoritos", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toastF.Show();
                    return;
                }

                List<Articulo> NList = new();
                foreach (Articulo A in ArticulosBusqueda)
                {
                    if (A.Sku == Art.Sku)
                        A.ImagenFavoritos = App.Funciones.Base64aImagen(App.Funciones.LogoFavoritosSolido);

                    NList.Add(A);
                }

                App.ServiciosBD.AgregarRegistroEntidadLocal(new TblFavoritos { SkuArticulo = Art.Sku, Nombre = Art.Nombre, Imagen = Art.Imagen64 });
                var toast = Toast.Make("Artículo agregado a favoritos", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();

                ArticulosBusqueda.Clear();
                foreach (Articulo A in NList)
                    ArticulosBusqueda.Add(A);
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                await App.Current.MainPage.DisplayAlert("Error", "Hubo un error al intentar agregar el artículo a favoritos.", "");
            }
        }

        /// <summary>
        /// Método para activar el frame de codigo postal
        /// </summary>
        private void MuestraInicioPopupCodigoPostal()
        {
            ColorFondoCodigoPostal = App.Funciones.GetColorResourceValue("ColorPopupFondoBotonCodigoPostal");
            ColorTextoCodigoPostal = App.Funciones.GetColorResourceValue("ColorPopupTextoBotonCodigoPostal");
            ColorFondoColonia = App.Funciones.GetColorResourceValue("ColorPopupFondoInicativoBotonCPyColonia");
            ColorTextoColonia = App.Funciones.GetColorResourceValue("ColorPopupTextoInicativoBotonCPyColonia");
            IsVisibleInicioPopupColonia = false;
            IsVisibleInicioPopupCodigoPostal = true;
        }

        /// <summary>
        /// Método para activar el frame de la colonia
        /// </summary>
        private void MuestraInicioPopupColonia()
        {
            ColorFondoCodigoPostal = App.Funciones.GetColorResourceValue("ColorPopupFondoInicativoBotonCPyColonia");
            ColorTextoCodigoPostal = App.Funciones.GetColorResourceValue("ColorPopupTextoInicativoBotonCPyColonia");
            ColorFondoColonia = App.Funciones.GetColorResourceValue("ColorPopupFondoBotonColonia");
            ColorTextoColonia = App.Funciones.GetColorResourceValue("ColorPopupTextoBotonColonia");
            IsVisibleInicioPopupCodigoPostal = false;
            IsVisibleInicioPopupColonia = true;
        }

        /// <summary>
        /// Método para agregar el artículo seleccionado al carrito
        /// </summary>
        /// <param name="Art">Objeto de tipo Artículo - Contiene la información del artículo seleccionado</param>
        private async void AgregarCarrito(Articulo Art)
        {
            try
            {
                if (App.ServiciosBD.ObtenerRegistroEntidadLocal("TblCarrito", Art.Sku).Count > 0)
                {
                    var toast1 = Toast.Make("Artículo ya esta en el carrito", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast1.Show();
                    return;
                }

                if (App.ServiciosBD.AgregarRegistroEntidadLocal(App.Funciones.ConvertirArticuloACarrito(Art, 1, "0", true, false)) > 0)
                {
                    var toast2 = Toast.Make("Artículo agregado al carrito", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast2.Show();
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                await App.Current.MainPage.DisplayAlert("Hola", "Tuvimos un problema para agregar el artículo al carrito, por favor intenta mas tarde.", "Aceptar");
                return;
            } 
        }

        /// <summary>
        /// Método para obtener los datos virtuales de empresa, sucursal, caja y cliente usados en varias operaciones de la APP.
        /// </summary>
        private async void ObtenDatosvirtuales(string Modalidad)
        {
            try
            {
                if (Modalidad.Equals("CP"))
                {
                    if (TextoCP.Trim().Length <= 4 || TextoCP.Trim().Equals("00000") || TextoCP.Trim().Equals("0"))
                    {
                        await App.Current.MainPage.DisplayAlert("Hola", "Debes ingresar un código postal válido, por favor, intenta nuevamente.", "Aceptar");
                        return;
                    }
                }
                else
                {
                    if (EstadoSel == null)
                    {
                        await App.Current.MainPage.DisplayAlert("Hola", "Debes seleccionar un estado, por favor, intenta nuevamente.", "Aceptar");
                        return;
                    }

                    if (MunicipioSel == null)
                    {
                        await App.Current.MainPage.DisplayAlert("Hola", "Debes seleccionar un municipio, por favor, intenta nuevamente.", "Aceptar");
                        return;
                    }

                    if (ColoniaSel == null)
                    {
                        await App.Current.MainPage.DisplayAlert("Hola", "Debes seleccionar una colonia, por favor, intenta nuevamente.", "Aceptar");
                        return;
                    }
                }

                TextoOverlay = "Validando...";
                App.Funciones.PopupEspera();

                int res = 0;
                int Emp = 1;
                int Suc = 1;
                int Caja = 1;
                long Cliente = 10000001;

                var ResBusqueda = await DStore.Consulta_SucursalUbicacion(1, Modalidad.Equals("CP") ? Convert.ToInt32(TextoCP) : Convert.ToInt32(ColoniaSel.CodigoPostal));
                if (ResBusqueda != null && !ResBusqueda.Equals(""))
                {
                    string[] Detalle = ResBusqueda.ToString().Split(",");
                    Emp = Convert.ToInt32(App.Funciones.BuscaValorArray(Detalle, "Emp_id"));
                    Suc = Convert.ToInt32(App.Funciones.BuscaValorArray(Detalle, "Suc_id"));
                    Caja = Convert.ToInt32(App.Funciones.BuscaValorArray(Detalle, "Caja_id"));
                    Cliente = Convert.ToInt32(App.Funciones.BuscaValorArray(Detalle, "Cliente"));
                }

                List<TblConfiguracion> configEmp = App.ServiciosBD.ObtenerRegistroEntidadLocal("TblConfiguracion", "EMPRESA_VIRTUAL").OfType<TblConfiguracion>().ToList();
                res = App.ServiciosBD.ActualizarRegistroEntidadLocal(new TblConfiguracion
                {
                    Id = configEmp[0].Id,
                    Nombre = configEmp[0].Nombre,
                    Descripcion = configEmp[0].Descripcion,
                    Valor = Emp.ToString()
                });

                if (res <= 0)
                {
                    App.Funciones.PopupCerrar();
                    await App.Current.MainPage.DisplayAlert("Hola", "No logramos procesar el código postal que ingresaste, intentalo de nuevo en esta opción o intenta mediante los datos de tu colonia.", "Aceptar");
                    return;
                }

                List<TblConfiguracion> configSuc = App.ServiciosBD.ObtenerRegistroEntidadLocal("TblConfiguracion", "SUCURSAL_VIRTUAL").OfType<TblConfiguracion>().ToList();
                res = App.ServiciosBD.ActualizarRegistroEntidadLocal(new TblConfiguracion
                {
                    Id = configSuc[0].Id,
                    Nombre = configSuc[0].Nombre,
                    Descripcion = configSuc[0].Descripcion,
                    Valor = Suc.ToString()
                });

                if (res <= 0)
                {
                    App.Funciones.PopupCerrar();
                    await App.Current.MainPage.DisplayAlert("Hola", "No logramos procesar el código postal que ingresaste, intentalo de nuevo en esta opción o intenta mediante los datos de tu colonia.", "Aceptar");
                    return;
                }

                List<TblConfiguracion> configCaja = App.ServiciosBD.ObtenerRegistroEntidadLocal("TblConfiguracion", "CAJA_VIRTUAL").OfType<TblConfiguracion>().ToList();
                res = App.ServiciosBD.ActualizarRegistroEntidadLocal(new TblConfiguracion
                {
                    Id = configCaja[0].Id,
                    Nombre = configCaja[0].Nombre,
                    Descripcion = configCaja[0].Descripcion,
                    Valor = Caja.ToString()
                });

                if (res <= 0)
                {
                    App.Funciones.PopupCerrar();
                    await App.Current.MainPage.DisplayAlert("Hola", "No logramos procesar el código postal que ingresaste, intentalo de nuevo en esta opción o intenta mediante los datos de tu colonia.", "Aceptar");
                    return;
                }

                List<TblConfiguracion> configCliente = App.ServiciosBD.ObtenerRegistroEntidadLocal("TblConfiguracion", "CLIENTE_VIRTUAL").OfType<TblConfiguracion>().ToList();
                res = App.ServiciosBD.ActualizarRegistroEntidadLocal(new TblConfiguracion
                {
                    Id = configCliente[0].Id,
                    Nombre = configCliente[0].Nombre,
                    Descripcion = configCliente[0].Descripcion,
                    Valor = Cliente.ToString()
                });

                if (res <= 0)
                {
                    App.Funciones.PopupCerrar();
                    await App.Current.MainPage.DisplayAlert("Hola", "No logramos procesar el código postal que ingresaste, intentalo mas tarde.", "Aceptar");
                    return;
                }

                App.Funciones.PopupCerrar();
                popCP.CerrarPopup();

                PopupPageBienvenida popBienvenida = new();
                App.Current.MainPage.ShowPopup(popBienvenida);
                timer = Dispatcher.CreateTimer();
                timer.Interval = TimeSpan.FromMilliseconds(3000);
                timer.Tick += (s, e) =>
                {
                    popBienvenida.CerrarPopup();
                    timer.Stop();
                };
                timer.Start();
            }
            catch (Exception ex)
            {
                App.Funciones.PopupCerrar();
                _ = ex.Message;
                await App.Current.MainPage.DisplayAlert("Hola", "Tuvimos un problema para procesar el código postal, por favor intenta mas tarde.", "Aceptar");
                return;
            }
        }

        private async void CargarEstados()
        {
            TextoOverlay = "Cargando Estados...";
            App.Funciones.PopupEspera();
            List<Estado> ListaEstado = await DStore.ObtenerEstadosApi();
            foreach (var Estado in ListaEstado)
                EstadosRepublica.Add(Estado);

            App.Funciones.PopupCerrar();
        }

        private async Task CargarMunicipios(Estado Estados)
        {
            TextoOverlay = "Cargando Municipios...";
            App.Funciones.PopupEspera();
            MunicipiosRepublica.Clear();
            List<Municipio> ListaMinucipios = await DStore.ObtenerMunicipiosApi(Estados.Nivel1_id);
            foreach (var Municipio in ListaMinucipios)
                MunicipiosRepublica.Add(Municipio);

            App.Funciones.PopupCerrar();
        }

        private async Task CargarColonias(Estado Estados, Municipio Municipios)
        {
            TextoOverlay = "Cargando Colonias...";
            App.Funciones.PopupEspera();
            ColoniasRepublica.Clear();
            List<Colonia> ListaColonias = await DStore.ObtenerColoniasApi(Estados.Nivel1_id, Municipios.Nivel2_id);
            foreach (var Colonias in ListaColonias)
                ColoniasRepublica.Add(Colonias);

            App.Funciones.PopupCerrar();
        }

        private async void MuestraDetalleArticulo(Articulo Art)
        {
            await Shell.Current.GoToAsync($"Caracteristicasart?sku={Art.Sku}&nombre={Art.Nombre}&precio={Art.Precio}");
        }
    }
}
