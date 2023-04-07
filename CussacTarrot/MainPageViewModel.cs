using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using CussacTarot.Core.Messages;
using CussacTarot.Core.Domains;
using CussacTarot.Gamers.Domains.Messages;
using CussacTarot.GameSheets.Domains.Messages;
using CussacTarot.Gamers.Domains;
using CussacTarot.Gamers.Presentations;

namespace CussacTarot;

public class MainPageViewModel : ObservableRecipient
{
    private readonly ILaunchGameService _LaunchGameService;
    
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

    private readonly ObservableCollection<GamerViewModel> _GamersChecked;
    public ObservableCollection<GamerViewModel> GamersChecked
    {
        get => _GamersChecked;
    }

    private IRelayCommand _LaunchGameCommand;
    public IRelayCommand LaunchGameCommand => _LaunchGameCommand ??= new RelayCommand(() =>
    {
        _LaunchGameService.Launch(_GamersChecked.Select(e => e.ToModel()));
        Messenger.Send(new RefreshGamesSheetsMessage());
    }, () => _GamersChecked.Count >= LaunchGameService.NUMBER_GAMER_BY_TABLE_FOUR);


    private IRelayCommand _ChangeViewCommand;
    public IRelayCommand ChangeViewCommand => _ChangeViewCommand ??= new RelayCommand(() =>
    {
        ShowGamer = !ShowGamer;
    });

    private bool _ShowGamer = true;
    public bool ShowGamer
    {
        get => _ShowGamer;
        set
        {
            SetProperty(ref _ShowGamer, value);
            OnPropertyChanged(nameof(NameButton));
        }
    }

    public string NameButton => ShowGamer ? "Afficher les parties" : "Afficher les joueurs";


    public MainPageViewModel(ILaunchGameService launchGameService)
    {
        _LaunchGameService = launchGameService ?? throw new ArgumentNullException(nameof(launchGameService));

        Messenger.Register<CreateOrUpdateGamerMessage>(this, (recipient, message) =>
            ShowPopUp?.Invoke(new EditGamerView(message.GamerViewModel)));

        //Messenger.Register<CreateOrUpdateGameSheetMessage>(this, (recipient, message) =>
        //{
        //    ElementInPopUp = new EditGameSheetView(message.GameSheetViewModel);
        //    IsOpenPopUp = true;
        //});
        Messenger.Register<FinishEditableGamerMessage>(this, (recipient, message) =>
        {
            ClosePopUp?.Invoke();
        });
        Messenger.Register<FinishEditableGameSheetMessage>(this, (recipient, message) =>
        {
            ClosePopUp?.Invoke();
        });

        _GamersChecked = new();
        _GamersChecked.CollectionChanged += _GamersChecked_CollectionChanged;
    }

    private void _GamersChecked_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        LaunchGameCommand.NotifyCanExecuteChanged();
    }
}

