using CommunityToolkit.Maui.Views;

namespace CussacTarot;

public partial class PopUp : Popup
{
    public PopUp(View view)
    {
        InitializeComponent();
        Content = view;
    }
}