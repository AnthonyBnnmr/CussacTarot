using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CussacTarot.Core.Domains;
using CussacTarot.Gamers.Domains.Messages;
using CussacTarot.Gamers.Presentations;
using CussacTarot.GameSheets.Domains.Messages;
using CussacTarot.GameSheets.Presentations;

namespace CussacTarot;

public class AppShellViewModel : ObservableRecipient
{
    
    private Func<View, Task<object?>> _ShowPopUp;
    public Func<View, Task<object?>> ShowPopUp
    {
        get => _ShowPopUp;
        set => SetProperty(ref _ShowPopUp, value);
    }

    private Action _ClosePopUp;
    public Action ClosePopUp
    {
        get => _ClosePopUp;
        set => SetProperty(ref _ClosePopUp, value);
    }   

    public AppShellViewModel()
    {       
        Messenger.Register<CreateOrUpdateGamerMessage>(this, (recipient, message) =>
            ShowPopUp?.Invoke(new EditGamerView(message.GamerViewModel)));

        Messenger.Register<CreateOrUpdateGameSheetMessage>(this, (recipient, message) =>
            ShowPopUp?.Invoke(new EditGameSheetView(message.GameSheetViewModel)));

        Messenger.Register<FinishEditableGamerMessage>(this, (recipient, message) =>
        {
            ClosePopUp?.Invoke();
        });

        Messenger.Register<FinishEditableGameSheetMessage>(this, (recipient, message) =>
        {
            ClosePopUp?.Invoke();
        });

        Messenger.Register<ChooseGamersMessage>(this, (recipient, message) =>
            ShowPopUp?.Invoke(new ListGamersView(message.ListGamerViewModel)));
    }

    public AppShellViewModel(ILaunchGameService launchGameService) : this()
    {
        
    }
}

