using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using CussacTarot.Core.Repositories;
using CussacTarot.Gamers.Domains;
using CussacTarot.Gamers.Domains.Messages;
using CussacTarot.Models;

namespace CussacTarot.Gamers.Presentations;

public class ListGamersViewModel : ObservableRecipient
{
    private IRepository<int, Gamer> _GamersRepository;

    private IRelayCommand _RemoveCommand;
    public IRelayCommand RemoveCommand => _RemoveCommand ??= new RelayCommand<IEnumerable<GamerViewModel>>((gamers) =>
    {
        foreach (GamerViewModel gamer in gamers.ToList())
        {
            if (_GamersRepository.Remove(gamer.ToModel()))
            {
                _Gamers.Remove(gamer);
            }
        }
    }, e =>
    {
        return e != null && e.Count() > 0;
    });

    public string NameButton => SelectedGamers == null || !SelectedGamers.Any() ? "Ajouter" : "Modifier";


    private IRelayCommand _CreateCommand;
    public IRelayCommand CreateCommand => _CreateCommand ??= new RelayCommand<IEnumerable<GamerViewModel>>((gamers) =>
    {
        Messenger.Send(new CreateOrUpdateGamerMessage(gamers?.FirstOrDefault()));
    });

    private IRelayCommand _RefreshNameButtonCommand;
    public IRelayCommand RefreshNameButtonCommand => _RefreshNameButtonCommand ??= new RelayCommand(() =>
    {
        OnPropertyChanged(nameof(NameButton));
    });

    private readonly ObservableCollection<GamerViewModel> _Gamers;
    private readonly ObservableCollection<GamerViewModel> _SelectedGamers;

    public ObservableCollection<GamerViewModel> Gamers => _Gamers;

    public IEnumerable<GamerViewModel> SelectedGamers
    {
        get => _SelectedGamers;
    }

    public ListGamersViewModel(IRepository<int, Gamer> gamersRepository)
    {
        _GamersRepository = gamersRepository ?? throw new ArgumentNullException(nameof(gamersRepository));
        Messenger.Register(this, (MessageHandler<object, FinishEditableGamerMessage>)((recipient, message) =>
        {
            InitListGamers();
            _SelectedGamers.Clear();
        }));
        _Gamers = new();        
        _SelectedGamers = new();
        _SelectedGamers.CollectionChanged += _SelectedGamers_CollectionChanged;
        InitListGamers();
    }    

    private void InitListGamers()
    {
        IEnumerable<int> gamerChecked = _Gamers.Where(e => e.Checked).Select(e => e.Id).ToList();        
        foreach (GamerViewModel gamerViewModel in _GamersRepository.GetAll().Select(g => new GamerViewModel(g)))
        {
            if (gamerChecked.Contains(gamerViewModel.Id))
            {
                gamerViewModel.Checked = true;
            }
            
            GamerViewModel f = _Gamers.FirstOrDefault(e => e.Id == gamerViewModel.Id);
            if(f != null)
            {
                _Gamers.Remove(f);
            }
            _Gamers.Add(gamerViewModel);
        }
    }

    private void _SelectedGamers_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        RemoveCommand.NotifyCanExecuteChanged();
    }
}

