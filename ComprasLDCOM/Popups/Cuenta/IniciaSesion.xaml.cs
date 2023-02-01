using CommunityToolkit.Maui.Views;

namespace ComprasLDCOM.Popups.Cuenta;

public partial class IniciaSesion : Popup
{
	public IniciaSesion()
	{
		InitializeComponent();
	}
    public void CerrarPopup()
    {
        Close();
    }
}