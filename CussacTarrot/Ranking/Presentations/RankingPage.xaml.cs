using CommunityToolkit.Mvvm.DependencyInjection;

namespace CussacTarot.Ranking.Presentations;

public partial class RankingPage : ContentPage
{
	public RankingPage()
	{
		InitializeComponent();
		BindingContext = Ioc.Default.GetRequiredService<RankingPageViewModel>();
	}
}