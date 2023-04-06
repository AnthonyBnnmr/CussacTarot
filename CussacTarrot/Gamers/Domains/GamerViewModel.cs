using CommunityToolkit.Mvvm.ComponentModel;
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
        set => SetProperty(ref _Name, value);
    }

    private string _Surname = string.Empty;
    public string Surname
    {
        get { return _Surname; }
        set => SetProperty(ref _Surname, value);
    }

    private bool _Checked;
    public bool Checked
    {
        get { return _Checked; }
        set => SetProperty(ref _Checked, value);

    }


    public GamerViewModel() : this(null)
    {
    }

    public GamerViewModel(Gamer gamer)
    {
        Name = gamer?.Name ?? string.Empty;
        Surname = gamer?.Surname ?? string.Empty;
        Id = gamer?.Id ?? 0;
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

