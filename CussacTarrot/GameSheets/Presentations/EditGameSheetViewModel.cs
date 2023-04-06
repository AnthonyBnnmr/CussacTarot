using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CussacTarot.GameSheets.Domains.Messages;
using CussacTarot.Core.Repositories;
using CussacTarot.GameSheets.Domains;
using CussacTarot.Models;

namespace CussacTarot.GameSheets.Presentations;

public class EditGameSheetViewModel : ObservableRecipient
{
    private GameSheetViewModel _GameSheet;
    private GameSheetViewModel _OldGameSheet;
    public GameSheetViewModel GameSheet
    {
        get
        {
            if (_GameSheet == null)
            {
                _GameSheet = new GameSheetViewModel();
                _OldGameSheet = new GameSheetViewModel();
            }

            return _GameSheet;
        }
        set
        {
            if (_GameSheet == null && value != null)
            {
                _OldGameSheet = value.Clone();
            }

            SetProperty(ref _GameSheet, value);
        }
    }

    private IRepository<int, GameSheet> _GameSheetsRepository;

    private IRelayCommand _ValidateCommand;
    public IRelayCommand ValidateCommand => _ValidateCommand ??= new RelayCommand(() =>
    {
        _GameSheet.End = DateTime.Now;
        _GameSheetsRepository.AddOrUpdate(_GameSheet.ToModel());
        Messenger.Send(new FinishEditableGameSheetMessage());
    });

    private IRelayCommand _CancelCommand;
    public IRelayCommand CancelCommand => _CancelCommand ??= new RelayCommand(() =>
    {
        _GameSheet = _OldGameSheet;
    });

    private IRelayCommand _CloseCommand;
    public IRelayCommand CloseCommand => _CloseCommand ??= new RelayCommand(() =>
    {
        Messenger.Send(new FinishEditableGameSheetMessage());
    });


    public EditGameSheetViewModel(IRepository<int, GameSheet> gameSheetsRepository)
    {
        _GameSheetsRepository = gameSheetsRepository ?? throw new ArgumentNullException(nameof(gameSheetsRepository));
    }
}

