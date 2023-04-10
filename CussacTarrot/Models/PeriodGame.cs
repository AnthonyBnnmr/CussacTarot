
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using CussacTarot.Core.Repositories;

namespace CussacTarot.Models
{
    
    public record PeriodGame
    {
        public DateTime? Start { get; init; }
        public DateTime? End { get; init; }
    }
}
