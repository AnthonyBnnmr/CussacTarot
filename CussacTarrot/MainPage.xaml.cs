using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace CussacTarot;

public partial class MainPage : ContentPage
{
    private PopUp _PopUp;

	public MainPage()
	{
		InitializeComponent();
        MainPageViewModel mainPageViewModel = Ioc.Default.GetRequiredService<MainPageViewModel>();
        mainPageViewModel.ShowPopUp = DisplayPopup;
        mainPageViewModel.ClosePopUp = () => _PopUp.Close(null);
        BindingContext = mainPageViewModel;
    }

    private Task<object?> DisplayPopup(View content)
    {
        _PopUp = new (content);
        return this.ShowPopupAsync(_PopUp);
    }
}

