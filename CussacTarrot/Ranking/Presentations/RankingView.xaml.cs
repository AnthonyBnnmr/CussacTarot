using CommunityToolkit.Mvvm.DependencyInjection;

namespace CussacTarot.Ranking.Presentations;

public partial class RankingView : ContentView
{
	public RankingView()
	{
		InitializeComponent();
		BindingContext = Ioc.Default.GetRequiredService<RankingViewModel>();
	}
}