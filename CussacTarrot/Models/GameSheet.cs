
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using CussacTarot.Core.Repositories;

namespace CussacTarot.Models
{

    public record ScoreByGamer : ValueRepository<int>
    {
        [AutoIncrement]
        public int Id { get; init; }
        public int GamerId { get; init; }
        public Gamer Gamer { get; init; }        
        public int Score { get; init; }
        public int GameSheetId { get; init; }
    }


    public record GameSheet : ValueRepository<int>
    {
        [AutoIncrement]
        public int Id { get; set; }
        public DateTime Created { get; init; }
        public DateTime? Start { get; init; }
        public DateTime? End { get; init; }

        [Reference]
        public List<ScoreByGamer> Scores { get; init; } = new List<ScoreByGamer>();
    }
}
