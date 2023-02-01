using ComprasLDCOM.API;
using ComprasLDCOM.BaseDeDatos;
using ComprasLDCOM.Paginas.Inicio;
using ComprasLDCOM.Paginas.Carrito;
using ComprasLDCOM.Paginas.Cuenta;
using ComprasLDCOM.Datos.Configuraciones.BaseDeDatos;
using ComprasLDCOM.Datos.Inicio.Response;
using ComprasLDCOM.Popups.Carrito;

namespace ComprasLDCOM;

public partial class App : Application
{
	private static ServicioBD ServBD;
    private static ApiFunciones ServFun;

    public App()
    {
        InitializeComponent();

        Routing.RegisterRoute("appShell", typeof(AppShell));
        Routing.RegisterRoute("Carritopage", typeof(CarritoPage));
        Routing.RegisterRoute("CuentaPage", typeof(CuentaPage));
        Routing.RegisterRoute("ConfirmacionRegistroPage", typeof(ConfirmacionRegistroPage));
        Routing.RegisterRoute("RestablecerContraseñaPage", typeof(RestablecerContraseñaPage));
        Routing.RegisterRoute("DireccionesPage", typeof(DireccionesPage));
        Routing.RegisterRoute("MiPerfilPage", typeof(MiPerfilPage));
        Routing.RegisterRoute("RegistroDatosPage", typeof(RegistroDatosPage));
        Routing.RegisterRoute("Iniciopage", typeof(InicioPage));
        Routing.RegisterRoute("Caracteristicasart", typeof(CaracteristicasArt));
        Routing.RegisterRoute("Busquedamonedero", typeof(PopupPageBusquedaMonedero));
        Routing.RegisterRoute("Payment", typeof(Payment));
        Routing.RegisterRoute("Totales", typeof(Totales));

        List<TblConfiguracion> ConfigApp = App.ServiciosBD.ObtenerListaEntidadLocal("TblConfiguracion").OfType<TblConfiguracion>().ToList();
        if (ConfigApp.Where(x => x.Nombre == "API").ToList().Count <= 0)
            App.ServiciosBD.AgregarRegistroEntidadLocal(new TblConfiguracion { Id = 1, Nombre = "API", Descripcion = "URL de conexion de la API con LDCOM", Valor = "http://187.188.102.33/RestApi_MOBILE_APPCliente" });

        if (ConfigApp.Where(x => x.Nombre == "EMPRESA_VIRTUAL").ToList().Count <= 0)
            App.ServiciosBD.AgregarRegistroEntidadLocal(new TblConfiguracion { Id = 2, Nombre = "EMPRESA_VIRTUAL", Descripcion = "Empresa virtual de eccommerce para usar en los procesos de la App", Valor = "" });

        if (ConfigApp.Where(x => x.Nombre == "SUCURSAL_VIRTUAL").ToList().Count <= 0)
            App.ServiciosBD.AgregarRegistroEntidadLocal(new TblConfiguracion { Id = 3, Nombre = "SUCURSAL_VIRTUAL", Descripcion = "Sucursal virtual de eccommerce para usar en los procesos de la App", Valor = "" });

        if (ConfigApp.Where(x => x.Nombre == "CAJA_VIRTUAL").ToList().Count <= 0)
            App.ServiciosBD.AgregarRegistroEntidadLocal(new TblConfiguracion { Id = 4, Nombre = "CAJA_VIRTUAL", Descripcion = "Caja virtual de eccommerce para usar en los procesos de la App", Valor = "" });

        if (ConfigApp.Where(x => x.Nombre == "CLIENTE_VIRTUAL").ToList().Count <= 0)
            App.ServiciosBD.AgregarRegistroEntidadLocal(new TblConfiguracion { Id = 5, Nombre = "CLIENTE_VIRTUAL", Descripcion = "Cliente virtual de eccommerce para usar en los procesos de la App", Valor = "" });

        if (ConfigApp.Where(x => x.Nombre == "LOGO_APP").ToList().Count <= 0)
            App.ServiciosBD.AgregarRegistroEntidadLocal(new TblConfiguracion { Id = 6, Nombre = "LOGO_APP", Descripcion = "Logo de la APP del cliente", Valor = App.Funciones.LogoApp });

        if (ConfigApp.Where(x => x.Nombre == "LADA").ToList().Count <= 0)
            App.ServiciosBD.AgregarRegistroEntidadLocal(new TblConfiguracion { Id = 7, Nombre = "LADA", Descripcion = "Lada paises", Valor = "https://gist.githubusercontent.com" });

        if (ConfigApp.Where(x => x.Nombre == "CP").ToList().Count <= 0)
            App.ServiciosBD.AgregarRegistroEntidadLocal(new TblConfiguracion { Id = 8, Nombre = "CP", Descripcion = "Copomex", Valor = "https://api.copomex.com" });

        MainPage = new AppShell();
    }

	public static ServicioBD ServiciosBD
	{
		get
		{
			if (ServBD == null)
				ServBD = new ServicioBD(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ComprasLDcomBD.db3"));

			return ServBD;
		}
	}

    public static ApiFunciones Funciones
    {
        get
        {
            if (ServFun == null)
                ServFun = new ApiFunciones();

            return ServFun;
        }
    }
}
