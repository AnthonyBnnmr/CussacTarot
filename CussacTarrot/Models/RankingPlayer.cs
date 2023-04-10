
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using CussacTarot.Core.Repositories;

namespace CussacTarot.Models
{
    
    public record RankingPlayer
    {        
        public Gamer Gamer { get; init; }
        public PeriodGame PeriodGame { get; init; }
        public int Score { get; init; }
    }
}
