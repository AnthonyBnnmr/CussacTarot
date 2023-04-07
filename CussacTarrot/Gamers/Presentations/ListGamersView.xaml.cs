using CommunityToolkit.Mvvm.DependencyInjection;
using CussacTarot.Gamers.Presentations;

namespace CussacTarot.Gamers.Presentations;

public partial class ListGamersView : ContentView
{
	public ListGamersView()
	{
		InitializeComponent();
        BindingContext = Ioc.Default.GetRequiredService<ListGamersViewModel>();
    }
}