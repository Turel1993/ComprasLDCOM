using ComprasLDCOM.Modelos.Cuenta;

namespace ComprasLDCOM.Paginas.Cuenta;

public partial class CuentaPage : ContentPage
{
	CuentaPageViewModel vm;
	public CuentaPage()
	{
        vm = new CuentaPageViewModel();
        BindingContext = vm = (Shell.Current as AppShell).cuentaPageView;
        InitializeComponent();
    }

}