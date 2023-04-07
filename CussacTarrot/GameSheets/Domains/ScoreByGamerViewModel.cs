using CommunityToolkit.Mvvm.ComponentModel;
using CussacTarot.Gamers.Domains;
using CussacTarot.Models;

namespace CussacTarot.GameSheets.Domains;

public class ScoreByGamerViewModel : ObservableObject
{
    private int _Id;
    public int Id
    {
        get => _Id;
        private set => SetProperty(ref _Id, value);
    }

    private GamerViewModel _Gamer;
    public GamerViewModel Gamer
    {
        get => _Gamer;
        private set => SetProperty(ref _Gamer, value);
    }

    private int? _GameSheetId;
    public int? GameSheetId
    {
        get => _GameSheetId;
        private set => SetProperty(ref _GameSheetId, value);
    }

    private int _Score;
    public int Score
    {
        get => _Score;
        set => SetProperty(ref _Score, value);
    }

    public ScoreByGamerViewModel() : this(null, null)
    {
    }

    public ScoreByGamerViewModel(ScoreByGamer scoreByGamer, int? idGameSheet)
    {
        Gamer = new GamerViewModel(scoreByGamer?.Gamer, null, null);
        Id = scoreByGamer?.Id ?? 0;
        GameSheetId = idGameSheet;
        Score = scoreByGamer?.Score ?? 0;
    }

    public ScoreByGamer ToModel()
    {
        return new ScoreByGamer
        {
            Gamer = Gamer.ToModel(),
            GamerId = Gamer.Id,
            Id = Id,
            GameSheetId = GameSheetId ?? 0,
            Score = Score
        };
    }
}

