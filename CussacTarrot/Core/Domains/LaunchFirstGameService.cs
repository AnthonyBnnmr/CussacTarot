using System;
using System.Collections.Generic;
using System.Linq;
using CussacTarot.Core.Repositories;
using CussacTarot.Models;

namespace CussacTarot.Core.Domains
{
    public class LaunchFirstGameService : LaunchGameService, ILaunchGameService<Gamer>
    {

        public LaunchFirstGameService(IRepository<int, GameSheet> gameSheetRepository)
            : base(gameSheetRepository) { }


        public void Launch(IEnumerable<Gamer> listGamers)
        {
            if (listGamers == null || !listGamers.Any())
            {
                return;
            }

            Random rand = new();
            List<Gamer> shuffledGamers = listGamers
                .OrderBy(x => rand.Next()).ToList();

            CreateGamesSheets(shuffledGamers);
        }
    }
}
