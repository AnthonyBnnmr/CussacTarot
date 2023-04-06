using CussacTarot.Core.Repositories;
using ServiceStack.DataAnnotations;

namespace CussacTarot.Models;

public record GameTable : ValueRepository<int>
{
    [AutoIncrement]
    public int Id { get; init; }

    [Index]
    public required int NumberOfGamer { get; init; }

    [Index]
    public required string Name { get; init; }

    [Reference]
    public List<Gamer> Gamers { get; init; } = new List<Gamer>();
}    
