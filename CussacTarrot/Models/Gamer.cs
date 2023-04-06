using ServiceStack.DataAnnotations;
using CussacTarot.Core.Repositories;

namespace CussacTarot.Models;

public record Gamer : ValueRepository<int>
{
    [AutoIncrement]
    [Index]
    public int Id { get; init; }

    [Index]
    public required string Name { get; init; }

    [Index]
    public required string Surname { get; init; }
}
