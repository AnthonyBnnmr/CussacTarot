using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CussacTarot.Core.Domains;
using CussacTarot.Core.Messages;
using CussacTarot.Gamers.Domains;
using System.Collections.ObjectModel;

namespace CussacTarot.Gamers.Presentations;

public class GamerPageViewModel : ObservableRecipient
{
    private readonly ILaunchGameService _LaunchGameService;

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


    public GamerPageViewModel()
    {
        _GamersChecked = new();
        _GamersChecked.CollectionChanged += GamersChecked_CollectionChanged;
    }

    public GamerPageViewModel(ILaunchGameService launchGameService) : this()
    {        
        _LaunchGameService = launchGameService ?? throw new ArgumentNullException(nameof(launchGameService));
    }

    private void GamersChecked_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        LaunchGameCommand.NotifyCanExecuteChanged();
    }
}

