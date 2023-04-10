using CommunityToolkit.Mvvm.DependencyInjection;
using CussacTarot.GameSheets.Domains;

namespace CussacTarot.GameSheets.Presentations;

public partial class EditGameSheetView : ContentView
{
    public EditGameSheetView() : this(null)
    {
    }

    public EditGameSheetView(GameSheetViewModel gameSheetViewModel)
    {
        InitializeComponent();
        EditGameSheetViewModel editGameSheetViewModel = Ioc.Default.GetRequiredService<EditGameSheetViewModel>();
        editGameSheetViewModel.GameSheet = gameSheetViewModel;
        BindingContext = editGameSheetViewModel;
    }
}