using ComprasLDCOM.Modelos.Cuenta;

namespace ComprasLDCOM.Paginas.Cuenta;

public partial class DireccionesPage : ContentPage
{
	DireccionesPageViewModel vm;
	public DireccionesPage()
	{
		vm = new DireccionesPageViewModel();
		InitializeComponent();
        BindingContext = vm;
    }
}