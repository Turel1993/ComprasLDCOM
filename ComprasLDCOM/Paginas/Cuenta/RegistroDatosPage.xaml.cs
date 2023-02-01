using ComprasLDCOM.Modelos.Cuenta;
using System.Diagnostics;

namespace ComprasLDCOM.Paginas.Cuenta;

public partial class RegistroDatosPage : ContentPage
{
    RegistroDatosViewModel vm;
    public RegistroDatosPage()
	{
        vm = new RegistroDatosViewModel();
        BindingContext = vm;
        InitializeComponent();
    }
    void OnButtonPressed(object sender, EventArgs args)
    {
        contra.IsPassword = false;
    }
    void OnButtonReleased(object sender, EventArgs args)
    {
        contra.IsPassword = true;
    }
    void OnButtonPressedConf(object sender, EventArgs args)
    {
        contraConfirma.IsPassword = false;
    }
    void OnButtonReleasedConf(object sender, EventArgs args)
    {
        contraConfirma.IsPassword = true;
    }
}