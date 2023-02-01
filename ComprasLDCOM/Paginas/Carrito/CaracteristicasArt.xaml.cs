using ComprasLDCOM.Modelos.Carrito;

namespace ComprasLDCOM.Paginas.Carrito;

public partial class CaracteristicasArt : ContentPage
{
	public CaracteristicasArt()
	{
		InitializeComponent();        
    }

    void OnScrollViewScrolled(object sender, ScrolledEventArgs e)
    {
        Console.WriteLine($"ScrollX: {e.ScrollX}, ScrollY: {e.ScrollY}");
    }
     
    private void btnBack_Clicked(object sender, EventArgs e)
    {
        //Remove the page on Top.
        int pageId = Application.Current.MainPage.Navigation.NavigationStack.Count - 1;
        this.Navigation.RemovePage(this.Navigation.NavigationStack[pageId]);
    }

    protected override bool OnBackButtonPressed()
    {
        return true;
    }
}