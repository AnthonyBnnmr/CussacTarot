using CommunityToolkit.Mvvm.DependencyInjection;
using CussacTarot.Gamers.Domains;

namespace CussacTarot.Gamers.Presentations;

public partial class EditGamerView : ContentView
{
	public EditGamerView() : this(null)
    {
		
	}

    public EditGamerView(GamerViewModel gamerViewModel)		
	{
        InitializeComponent();
        EditGamerViewModel editGamerViewModel = Ioc.Default.GetRequiredService<EditGamerViewModel>();
        editGamerViewModel.Gamer = gamerViewModel;
        BindingContext = editGamerViewModel;
    }
    
}