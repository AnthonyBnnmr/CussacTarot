using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CussacTarot.Gamers.Domains.Messages;
using CussacTarot.Models;
using CussacTarot.Core.Repositories;
using CussacTarot.Gamers.Domains;

namespace CussacTarot.Gamers.Presentations;

public class EditGamerViewModel : ObservableRecipient
{
    private GamerViewModel _Gamer;
    private GamerViewModel _OldGamer;
    public GamerViewModel Gamer
    {
        get
        {
            return _Gamer;
        }
        set
        {
            if (_Gamer == null && value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (_Gamer == null && value != null)
            {
                _OldGamer = value.Clone();
            }

            SetProperty(ref _Gamer, value);
        }
    }

    private IRepository<int, Gamer> _GamersRepository;

    private IRelayCommand _ValidateCommand;
    public IRelayCommand ValidateCommand => _ValidateCommand ??= new RelayCommand(() =>
    {
        for (int i = 0; i < 5; i++)
        {
            _Gamer.Surname = $"{_Gamer.Surname}_{i}";
            _Gamer.Name = $"{_Gamer.Name}_{i}";
            _GamersRepository.AddOrUpdate(_Gamer.ToModel());
        }

        Messenger.Send(new FinishEditableGamerMessage());
    });

    private IRelayCommand _CancelCommand;
    public IRelayCommand CancelCommand => _CancelCommand ??= new RelayCommand<Gamer>((gamer) =>
    {
        _Gamer = _OldGamer;
    });

    private IRelayCommand _CloseCommand;
    public IRelayCommand CloseCommand => _CloseCommand ??= new RelayCommand(() =>
    {
        Messenger.Send(new FinishEditableGamerMessage());
    });

    public EditGamerViewModel()
    {
    }

    public EditGamerViewModel(IRepository<int, Gamer> gamersRepository)
    {
        _GamersRepository = gamersRepository ?? throw new ArgumentNullException(nameof(gamersRepository));
    }
}

