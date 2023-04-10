using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CussacTarot.GameSheets.Domains.Messages;
using CussacTarot.Core.Messages;
using CussacTarot.Core.Repositories;
using CussacTarot.GameSheets.Domains;
using CussacTarot.Models;
using System.Collections.ObjectModel;

namespace CussacTarot.GameSheets.Presentations;

public class ListGameSheetsViewModel : ObservableRecipient
{
    private readonly IRepository<int, GameSheet> _GameSheetRepository;

    private readonly ObservableCollection<GameSheetViewModel> _GameSheets;
    public IEnumerable<GameSheetViewModel> GameSheets => _GameSheets;

    private GameSheetViewModel _SelectedGameSheet;
    public GameSheetViewModel SelectedGameSheet
    {
        get => _SelectedGameSheet;
        set => SetProperty(ref _SelectedGameSheet, value);
    }

    private IRelayCommand<GameSheetViewModel> _StartCommand;
    public IRelayCommand<GameSheetViewModel> StartCommand => _StartCommand ??= new RelayCommand<GameSheetViewModel>((gameSheet) =>
    {
        gameSheet.Start = DateTime.Now;
        _GameSheetRepository.AddOrUpdate(gameSheet.ToModel());       
        foreach (GameSheetViewModel gameSheetVM in GameSheets)
        {
            gameSheetVM.StartCommand.NotifyCanExecuteChanged();
            gameSheetVM.FinnishCommand.NotifyCanExecuteChanged();
        }
    }, (gameSheet) => gameSheet != null && gameSheet.Start == null);

    private IRelayCommand<GameSheetViewModel> _FinishCommand;
    public IRelayCommand<GameSheetViewModel> FinishCommand => _FinishCommand ??= new RelayCommand<GameSheetViewModel>((gameSheet) =>
    {
        if(gameSheet == null)
        {
            gameSheet = new GameSheetViewModel(null, StartCommand, FinishCommand);
        }

        Messenger.Send(new CreateOrUpdateGameSheetMessage(gameSheet));       
        foreach(GameSheetViewModel gameSheetVM in GameSheets)
        {
            gameSheetVM.StartCommand.NotifyCanExecuteChanged();
            gameSheetVM.FinnishCommand.NotifyCanExecuteChanged();
        }
    }, (gameSheet) => gameSheet != null && gameSheet.Start != null && !gameSheet.IsFinish);


    public ListGameSheetsViewModel()
    {
    }

    public ListGameSheetsViewModel(IRepository<int, GameSheet> gameSheetRepository)
    {
        _GameSheetRepository = gameSheetRepository ?? throw new ArgumentNullException(nameof(gameSheetRepository));
        _GameSheets = new();
        InitListGameSheets();

        Messenger.Register<RefreshGamesSheetsMessage>(this, (recipient, message) =>
        {
            InitListGameSheets();
        });

        Messenger.Register<FinishEditableGameSheetMessage>(this, (recipient, message) =>
        {
            InitListGameSheets();
        });
    }

    private void InitListGameSheets()
    {
        _GameSheets.Clear();        
        foreach (GameSheet gameSheet in _GameSheetRepository.GetAll())
        {
            _GameSheets.Add(new GameSheetViewModel(gameSheet, StartCommand, FinishCommand));
        }
    }
}

