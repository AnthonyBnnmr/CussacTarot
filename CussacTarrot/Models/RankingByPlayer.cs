
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using CussacTarot.Core.Repositories;

namespace CussacTarot.Models
{
    
    public record GameScore
    {
        public PeriodGame PeriodGame { get; init; }
        public int Score { get; init; }
    }

    public record RankingByPlayer
    {
        public int IdGamer { get; init; }        
        public required string NameSurname { get; init; }
        public required IEnumerable<GameScore> GameScore { get; init; }
        public int ScoreTotal => GameScore?.Sum(e => e.Score) ?? 0;
    }
}
