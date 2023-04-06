using System;
using System.Collections.Generic;
using System.Linq;
using CussacTarot.Core.Repositories;
using CussacTarot.Models;

namespace CussacTarot.Core.Domains
{
    public class LaunchSecondGameService : LaunchGameService, ILaunchGameService<GameSheet>
    {
        public LaunchSecondGameService(IRepository<int, GameSheet> gameSheetRepository)
            : base(gameSheetRepository) { }

        public void Launch(IEnumerable<GameSheet> listGameSheet)
        {
            if(listGameSheet == null || !listGameSheet.Any())
            {
                return;
            }

            IEnumerable<Gamer> gamers = listGameSheet.SelectMany(e => e.Scores)
                .OrderBy(e => e.Score).Select(e => e.Gamer);

            CreateGamesSheets(gamers);
        }
    }
}
