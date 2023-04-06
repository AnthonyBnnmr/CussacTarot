using System;
using System.Collections.Generic;

namespace CussacTarot.Core.Repositories;

public interface IRepository<Key, Value> : IDisposable
    where Value : ValueRepository<Key>
{
    bool AddOrUpdate(Value element);
    bool Remove(Value element);

    IEnumerable<Value> GetAll();

    Value Find(Key key);

    bool Clear();
}
