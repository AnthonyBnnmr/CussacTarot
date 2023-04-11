using CommunityToolkit.Mvvm.DependencyInjection;

namespace CussacTarot.Ranking.Presentations;

public partial class RankingNowPage : ContentPage
{
	public RankingNowPage()
	{
		InitializeComponent();
		BindingContext = Ioc.Default.GetRequiredService<RankingPageViewModel>();
	}
}