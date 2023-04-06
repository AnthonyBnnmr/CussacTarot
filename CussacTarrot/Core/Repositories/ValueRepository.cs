using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CussacTarot.Core.Repositories
{
    public interface ValueRepository<Key>
    {
        Key Id { get; }
    }
}
