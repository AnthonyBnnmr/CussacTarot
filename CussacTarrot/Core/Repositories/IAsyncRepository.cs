using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CussacTarot.Core.Repositories;

public interface IAsyncRepository<Key, Value> : IDisposable
    where Value : ValueRepository<Key>
{
    Task<bool> AddOrUpdateAsync(Value element);    
    Task<bool> RemoveAsync(Value element);

    Task<IEnumerable<Value>> GetAllAsync();

    Task<Value> FindAsync(Key key);

    Task<bool> ClearAsync();
}
