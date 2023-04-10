//using Android.OS;
using ClienteMAUI.ConexionDatos;
using ClienteMAUI.Models;
using System.Diagnostics;

namespace ClienteMAUI.Pages;
[QueryProperty(nameof(plato),"Plato")]
public partial class GestionPlatosPage : ContentPage
{
    private readonly IRestConexionDatos conexionDatos;
	private Plato _plato;
	private bool _esNuevo;//si el plato es nuevo
	public Plato plato {
		get => _plato;
		set {
			_esNuevo = esNuevo(value);
			_plato = value;
			OnPropertyChanged();//Obligatorio al actualizar un plato existente
		}
	}
    public GestionPlatosPage(IRestConexionDatos conexionDatos)
	{
		InitializeComponent();
		this.conexionDatos = conexionDatos;
		BindingContext = this;//Para enlazar datod de C# a xaml (es obligatorio)
	}
	bool esNuevo(Plato plato) {
		return plato.Id == 0 ? true : false;
	}
	async void OnCancelarClic(object sender, EventArgs e) {
		await Shell.Current.GoToAsync("..");
	}
	async void OnGuardarClic(object sender, EventArgs e)
	{
		if (_esNuevo)
		{
			Debug.WriteLine("[REGISTRO] Agregando un nuevo plato");
			await conexionDatos.AddPlatoAsync(plato);
		}
		else {
            Debug.WriteLine("[REGISTRO] Modificando un plato");
            await conexionDatos.UpdatePlatoAsync(plato);
        }
        await Shell.Current.GoToAsync("..");
    }
	async void OnEliminarClic(object sender, EventArgs e) {
		await conexionDatos.DeletePlatoAsync(plato.Id);
        await Shell.Current.GoToAsync("..");
    }
}