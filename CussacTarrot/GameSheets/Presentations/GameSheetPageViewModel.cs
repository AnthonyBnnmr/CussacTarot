using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CussacTarot.GameSheets.Domains.Messages;
using CussacTarot.Core.Repositories;
using CussacTarot.GameSheets.Domains;
using CussacTarot.Models;
using CussacTarot.Core.Messages;
using System.Collections.ObjectModel;
using CussacTarot.Core.Domains;
using CussacTarot.Gamers.Domains.Messages;
using ServiceStack;
using CussacTarot.Gamers.Domains;

namespace CussacTarot.GameSheets.Presentations;

public class GameSheetPageViewModel : ObservableRecipient
{
    private readonly ILaunchGameService _LaunchGameService;

    private readonly ObservableCollection<ScoreByGamerViewModel> _ScoreByGamer;
    public ObservableCollection<ScoreByGamerViewModel> ScoreByGamer
    {
        get => _ScoreByGamer;
    }

    private IRelayCommand _LaunchGameCommand;
    public IRelayCommand LaunchGameCommand => _LaunchGameCommand ??= new RelayCommand(() =>
    {
        Messenger.Send(new ChooseGamersMessage(_ScoreByGamer.Select(e => e.Gamer)));
        //_LaunchGameService.Launch(_ScoreByGamer);        
    }, () => _ScoreByGamer.Count >= LaunchGameService.NUMBER_GAMER_BY_TABLE_FOUR);

    public GameSheetPageViewModel()
    {
        _ScoreByGamer = new();
        _ScoreByGamer.CollectionChanged += Gamers_CollectionChanged;
    }

    public GameSheetPageViewModel(ILaunchGameService launchGameService, IRepository<int, GameSheet> gameSheetRepository) : this()
    {
        _LaunchGameService = launchGameService ?? throw new ArgumentNullException(nameof(launchGameService));
        InitScoreGames(gameSheetRepository);
        Messenger.Register(this, (MessageHandler<object, FinishEditableGameSheetMessage>)((recipient, message) =>
        {
            InitScoreGames(gameSheetRepository);
        }));
    }

    private void InitScoreGames(IRepository<int, GameSheet> gameSheetRepository)
    {
        _ScoreByGamer.Clear();
        _ScoreByGamer.AddDistinctRange(gameSheetRepository.GetAll().Where(d => d.PeriodGame?.End.HasValue ?? false && d.PeriodGame.End.Value.Date == DateTime.Now.Date)
                    .SelectMany(e => e.Scores).Select(e => new ScoreByGamerViewModel(e, null)));
    }

    private void Gamers_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        LaunchGameCommand.NotifyCanExecuteChanged();
    }    
}

