using CommunityToolkit.Mvvm.DependencyInjection;

namespace CussacTarot.GameSheets.Presentations;

public partial class ListGameSheetsView : ContentView
{
	public ListGameSheetsView()
	{
		InitializeComponent();
		BindingContext = Ioc.Default.GetRequiredService<ListGameSheetsViewModel>();
	}
}