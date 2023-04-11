using System;
using System.Collections.Generic;
using System.Linq;
using CussacTarot.Core.Repositories;
using CussacTarot.Models;

namespace CussacTarot.Core.Domains
{
    public class LaunchSecondGameService : LaunchGameService, ILaunchGameService<ScoreByGamer>
    {
        public LaunchSecondGameService(IRepository<int, GameSheet> gameSheetRepository)
            : base(gameSheetRepository) { }

        public void Launch(IEnumerable<ScoreByGamer> listScores)
        {
            if (listScores == null || !listScores.Any())
            {
                return;
            }

            IEnumerable<Gamer> gamers = listScores.OrderBy(e => e.Score).Select(e => e.Gamer);

            CreateGamesSheets(gamers);
        }
    }
}
