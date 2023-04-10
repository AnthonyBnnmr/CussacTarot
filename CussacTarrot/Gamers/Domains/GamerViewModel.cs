using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CussacTarot.Core;
using CussacTarot.Models;

namespace CussacTarot.Gamers.Domains;

public class GamerViewModel : ObservableObject, IClone<GamerViewModel>
{
    private int _Id;
    public int Id
    {
        get { return _Id; }
        private set => SetProperty(ref _Id, value);
    }

    private string _Name = string.Empty;
    public string Name
    {
        get { return _Name; }
        set
        {
            SetProperty(ref _Name, value);
            OnPropertyChanged(nameof(NameSurname));
        }
    }

    public string NameSurname => $"{Name} {Surname}";

    private string _Surname = string.Empty;
    public string Surname
    {
        get { return _Surname; }
        set
        {
            SetProperty(ref _Surname, value);
            OnPropertyChanged(nameof(NameSurname));
        }
    }

    private bool _Checked;
    public bool Checked
    {
        get { return _Checked; }
        set => SetProperty(ref _Checked, value);

    }

    private IRelayCommand _RemoveCommand;
    public IRelayCommand RemoveCommand => _RemoveCommand;

    private IRelayCommand _CreateCommand;
    public IRelayCommand CreateCommand => _CreateCommand;


    public GamerViewModel() : this(null, null, null)
    {
    }

    public GamerViewModel(Gamer gamer, IRelayCommand<GamerViewModel> createCommand, IRelayCommand<GamerViewModel> removeCommand)
    {
        Name = gamer?.Name ?? string.Empty;
        Surname = gamer?.Surname ?? string.Empty;
        Id = gamer?.Id ?? 0;
        _CreateCommand = createCommand != null ? new RelayCommand(() => createCommand.Execute(this), () => createCommand.CanExecute(this)) : null;
        _RemoveCommand = removeCommand != null ? new RelayCommand(() => removeCommand.Execute(this), () => removeCommand.CanExecute(this)) : null;
    }

    public Gamer ToModel()
    {
        return new Gamer
        {
            Name = Name,
            Surname = Surname,
            Id = Id
        };
    }

    public GamerViewModel Clone() => MemberwiseClone() as GamerViewModel;
}

