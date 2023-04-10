using ClienteMAUI.Pages;

namespace ClienteMAUI;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(GestionPlatosPage), typeof(GestionPlatosPage));
	}
}
