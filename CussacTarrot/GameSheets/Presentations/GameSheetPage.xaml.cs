using CommunityToolkit.Mvvm.DependencyInjection;

namespace CussacTarot.GameSheets.Presentations;

public partial class GameSheetPage : ContentPage
{
	public GameSheetPage()
	{
		InitializeComponent();
		BindingContext = Ioc.Default.GetRequiredService<GameSheetPageViewModel>();
	}
}