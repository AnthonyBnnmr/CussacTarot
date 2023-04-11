using CommunityToolkit.Mvvm.DependencyInjection;

namespace CussacTarot.Ranking.Presentations;

public partial class RankingView : ContentView
{
    public static readonly BindableProperty IsNowProperty =
        BindableProperty.CreateAttached("IsNow", typeof(bool), typeof(RankingView), false, propertyChanged: OnNowChanged);
    

    public static bool GetIsNow(BindableObject view)
    {
        return (bool)view.GetValue(IsNowProperty);
    }

    public static void SetIsNow(BindableObject view, bool value)
    {
        view.SetValue(IsNowProperty, value);
    }

    private static void OnNowChanged(BindableObject view, object oldValue, object newValue)
    {
      ((RankingViewModel)view.BindingContext).IsNow = (bool)newValue;
        
    }

    public RankingView()
    {
        InitializeComponent();        
        BindingContext = Ioc.Default.GetRequiredService<RankingViewModel>();
    }
}