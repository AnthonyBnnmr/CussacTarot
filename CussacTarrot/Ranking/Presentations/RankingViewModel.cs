using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CussacTarot.Core.Domains;
using CussacTarot.Core.Messages;
using CussacTarot.Models;
using ServiceStack;
using System.Collections.ObjectModel;

namespace CussacTarot.Ranking.Presentations;

public class RankingViewModel : ObservableRecipient
{
    private readonly RankingService _RankingService;

    private readonly ObservableCollection<RankingByPlayer> _RankingPlayers;
    public IEnumerable<RankingByPlayer> RankingPlayers => _RankingPlayers;


    private IRelayCommand _RefreshCommand;
    public IRelayCommand RefreshCommand => _RefreshCommand ??= new RelayCommand(() =>
    {
        InitListRankingPlayer();
    });

    public RankingViewModel()
    {
    }

    public RankingViewModel(RankingService rankingService)
    {
        _RankingService = rankingService ?? throw new ArgumentNullException(nameof(rankingService));
        _RankingPlayers = new();
        InitListRankingPlayer();

        Messenger.Register<RefreshRankingMessage>(this, (recipient, message) =>
        {
            InitListRankingPlayer();
        });        
    }

    private void InitListRankingPlayer()
    {
        _RankingPlayers.Clear();
        _RankingPlayers.AddDistinctRange(_RankingService.GetRankingbyPlayer());
    }
}

