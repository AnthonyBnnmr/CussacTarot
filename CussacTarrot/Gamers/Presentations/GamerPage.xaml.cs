using CommunityToolkit.Mvvm.DependencyInjection;

namespace CussacTarot.Gamers.Presentations;

public partial class GamerPage : ContentPage
{
	public GamerPage()
	{
		InitializeComponent();
		BindingContext = Ioc.Default.GetRequiredService<GamerPageViewModel>();
	}
}