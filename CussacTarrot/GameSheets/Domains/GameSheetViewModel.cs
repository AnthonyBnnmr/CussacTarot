using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CussacTarot.Models;
using CussacTarot.Core;
using CommunityToolkit.Mvvm.Input;

namespace CussacTarot.GameSheets.Domains;

public class GameSheetViewModel : ObservableObject, IClone<GameSheetViewModel>
{
    private int _Id;
    public int Id
    {
        get => _Id;
        private set => SetProperty(ref _Id, value);
    }


    private DateTime _Created;
    public DateTime Created
    {
        get => _Created;
        set => SetProperty(ref _Created, value);
    }

    private DateTime? _Start;
    public DateTime? Start
    {
        get => _Start;
        set => SetProperty(ref _Start, value);
    }

    private DateTime? _End;
    public DateTime? End
    {
        get => _End;
        set
        {
            SetProperty(ref _End, value);
            OnPropertyChanged(nameof(IsFinish));
        }
    }

    public bool IsFinish
    {
        get => _End != null;
    }

    private IRelayCommand _StartCommand;
    public IRelayCommand StartCommand => _StartCommand;

    private IRelayCommand _FinnishCommand;
    public IRelayCommand FinnishCommand => _FinnishCommand;


    private readonly ObservableCollection<ScoreByGamerViewModel> _Scores;
    public ObservableCollection<ScoreByGamerViewModel> Scores => _Scores;

    public GameSheetViewModel() : this(null, null, null)
    {
    }

    public GameSheetViewModel(GameSheet gameSheet, IRelayCommand<GameSheetViewModel> startCommand, IRelayCommand<GameSheetViewModel> finnishCommand)
    {
        Id = gameSheet?.Id ?? 0;
        Created = gameSheet?.Created ?? DateTime.MinValue;
        Start = gameSheet?.PeriodGame?.Start;
        End = gameSheet?.PeriodGame?.End;
        _Scores = new();
        foreach (ScoreByGamer scoreByGamer in gameSheet?.Scores ?? new())
        {
            Scores.Add(new ScoreByGamerViewModel(scoreByGamer, Id));
        }
        _StartCommand = startCommand == null ? throw new ArgumentNullException(nameof(startCommand)) : new RelayCommand(() => startCommand.Execute(this), () => startCommand.CanExecute(this));
        _FinnishCommand = finnishCommand == null ? throw new ArgumentNullException(nameof(finnishCommand)) : new RelayCommand(() => finnishCommand.Execute(this), () => finnishCommand.CanExecute(this));

    }

    public GameSheet ToModel()
    {
        return new GameSheet
        {
            Id = Id,
            Created = Created,
            PeriodGame = new()
            {
                Start = Start,
                End = End
            },
            Scores = Scores.Select(e => e.ToModel()).ToList(),
        };
    }

    public GameSheetViewModel Clone()
    {
        return MemberwiseClone() as GameSheetViewModel;
    }
}

