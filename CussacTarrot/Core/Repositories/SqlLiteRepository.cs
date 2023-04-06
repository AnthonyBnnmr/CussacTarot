using ServiceStack.OrmLite;
using ServiceStack.Data;
using System.Data;

namespace CussacTarot.Core.Repositories;

public class SqlLiteRepository<Key, Value> : IRepository<Key, Value>, IAsyncRepository<Key, Value>
    where Value : ValueRepository<Key>
{
    private readonly IDbConnection _DbConnection;
    private IDbTransaction _DbTransaction => _DbConnection.OpenTransactionIfNotExists();


    public SqlLiteRepository(IDbConnectionFactory dbConnectionFactory)
    {
        if (dbConnectionFactory == null)
        {
            throw new ArgumentNullException(nameof(dbConnectionFactory));
        }
        _DbConnection = dbConnectionFactory.OpenDbConnection();
    }

    public bool AddOrUpdate(Value element)
    {
        return _DbConnection.Save(element, true);
    }

    public bool Remove(Value element)
    {
        return _DbConnection.Delete(element) > 0;
    }

    public IEnumerable<Value> GetAll() => _DbConnection.LoadSelect<Value>();

    public Value Find(Key key) => _DbConnection.LoadSingleById<Value>(key);

    public Task<Value> FindAsync(Key key) => _DbConnection.LoadSingleByIdAsync<Value>(key);

    public void Dispose()
    {
        _DbConnection?.Dispose();
    }

    public Task<bool> AddOrUpdateAsync(Value element)
    {
        return _DbConnection.SaveAsync(element, true);
    }

    public async Task<bool> RemoveAsync(Value element)
    {
        return (await _DbConnection.DeleteAsync(element)) > 0;
    }

    public async Task<IEnumerable<Value>> GetAllAsync()
    {
        return await _DbConnection.LoadSelectAsync<Value>(t => t != null);
    }

    public async Task<bool> ClearAsync()
    {
        return (await _DbConnection.DeleteAllAsync<Value>()) > 0;
    }

    public bool Clear()
    {
        return _DbConnection.DeleteAll<Value>() > 0;
    }
}