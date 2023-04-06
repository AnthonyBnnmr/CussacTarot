using System.Collections.Generic;
using CussacTarot.Models;

namespace CussacTarot.Core.Domains
{
    public interface ILaunchGameService<T>
    {
        void Launch(IEnumerable<T> element);
    }
}