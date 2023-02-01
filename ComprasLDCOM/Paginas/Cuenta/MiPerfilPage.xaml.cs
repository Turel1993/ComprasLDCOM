using ComprasLDCOM.Modelos.Cuenta;

namespace ComprasLDCOM.Paginas.Cuenta;

public partial class MiPerfilPage : ContentPage
{
    MiPerfilViewModel vm;
	public MiPerfilPage()
	{
				 
        vm = new MiPerfilViewModel();
		BindingContext = vm;
		InitializeComponent();
		
	}
}