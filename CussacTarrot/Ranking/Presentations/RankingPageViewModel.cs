using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CussacTarot.Core.Domains;
using CussacTarot.Core.Messages;
using CussacTarot.Models;
using ServiceStack;
using System.Collections.ObjectModel;

namespace CussacTarot.Ranking.Presentations;

public class RankingPageViewModel : ObservableRecipient
{    
    public RankingPageViewModel()
    {
    }
}

