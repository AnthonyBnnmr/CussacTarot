using CommunityToolkit.Mvvm.DependencyInjection;
using CussacTarot.Gamers.Domains;
using CussacTarot.Gamers.Presentations;
using ServiceStack;

namespace CussacTarot.Gamers.Presentations;

public partial class ListGamersView : ContentView
{
    private readonly ListGamersViewModel _ListGamersViewModel;

    public ListGamersView()
	{
		InitializeComponent();
        _ListGamersViewModel = Ioc.Default.GetRequiredService<ListGamersViewModel>();
        BindingContext = _ListGamersViewModel;
    }


    public ListGamersView(IEnumerable<GamerViewModel> gamerViewModels) : this()
    {
        _ListGamersViewModel.Gamers.Clear();
        _ListGamersViewModel.Gamers.AddDistinctRange(gamerViewModels);
    }
}