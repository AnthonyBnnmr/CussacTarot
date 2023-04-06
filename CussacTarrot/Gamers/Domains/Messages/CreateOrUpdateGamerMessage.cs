using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CussacTarot.Gamers.Domains.Messages
{
    public record CreateOrUpdateGamerMessage(GamerViewModel GamerViewModel)
    {
    }
}
