using CommunityToolkit.Maui.Views;
using ComprasLDCOM.API;

namespace ComprasLDCOM.Popups.Cuenta;

public partial class AvisoPopUp : Popup
{
	ApiDataStore store = new();
	public AvisoPopUp()
	{
		InitializeComponent();
		store.getAvisoPrivacidad().GetAwaiter();
	}



}