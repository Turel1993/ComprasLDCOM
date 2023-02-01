using ComprasLDCOM.Modelos.Carrito;

namespace ComprasLDCOM.Paginas.Carrito;

public partial class Payment : ContentPage
{    
    public Payment()
	{
        InitializeComponent();
        BindingContext = new PaymentViewModel();     
    }

    protected override bool OnBackButtonPressed()
    {
        return true;
    }

    private void MenuButton_Clicked(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }

    private void btnBack_Clicked(object sender, EventArgs e)
    {        
        //Remove the page on Top.
        int pageId = Application.Current.MainPage.Navigation.NavigationStack.Count - 1;
        this.Navigation.RemovePage(this.Navigation.NavigationStack[pageId]);
    }
}