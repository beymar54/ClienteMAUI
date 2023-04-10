//using Android.OS;
using ClienteMAUI.ConexionDatos;
using ClienteMAUI.Models;
using ClienteMAUI.Pages;
using System.Diagnostics;
//using Java.Lang;

namespace ClienteMAUI;
public partial class MainPage : ContentPage
{
    private readonly IRestConexionDatos conexionDatos;

    public MainPage(IRestConexionDatos conexionDatos)
	{
		InitializeComponent();
		this.conexionDatos = conexionDatos;
	}
	protected async override void OnAppearing()
	{
		base.OnAppearing();
		coleccionPlatosView.ItemsSource = await conexionDatos.GetPlatosAsync();
	}
	//Evento Add
	async void OnAddPlatoClic(object sender, EventArgs e)
	{
		var param = new Dictionary<string, object> {
			{ nameof(Plato), new Plato()}
		};
		await Shell.Current.GoToAsync(nameof(GestionPlatosPage), param);
		Debug.WriteLine("[EVENTO] Se hizo clic en Agregar plato.");
	}
    //Evento clic a un plato
    async void OnElementoCambiado(object sender, SelectionChangedEventArgs e)
    {
        var param = new Dictionary<string, object> {
            { nameof(Plato), e.CurrentSelection.FirstOrDefault() as Plato}
        };
        await Shell.Current.GoToAsync(nameof(GestionPlatosPage), param);
        Debug.WriteLine("[EVENTO] Se hizo clic en algun plato.");
    }
}

