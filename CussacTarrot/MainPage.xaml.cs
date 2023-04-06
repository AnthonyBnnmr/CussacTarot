using CommunityToolkit.Mvvm.DependencyInjection;

namespace CussacTarot;

public partial class MainPage : ContentPage
{	
	public MainPage()
	{
		InitializeComponent();
		BindingContext = Ioc.Default.GetRequiredService<MainPageViewModel>();
	}
}

