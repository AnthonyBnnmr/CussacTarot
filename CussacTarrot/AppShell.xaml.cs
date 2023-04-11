using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace CussacTarot;

public partial class AppShell : Shell
{
	private PopUp _PopUp;


    public AppShell()
	{
		InitializeComponent();
        AppShellViewModel appShellViewModel = Ioc.Default.GetRequiredService<AppShellViewModel>();
		appShellViewModel.ShowPopUp = ShowPopUp;
        appShellViewModel.ClosePopUp = () => _PopUp.Close();
		BindingContext = appShellViewModel;
    }

	public Task<object?> ShowPopUp(View view)
	{
        _PopUp = new (view);
		return this.ShowPopupAsync(_PopUp);
	}
}
