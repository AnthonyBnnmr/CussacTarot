﻿using CussacTarot.Models;

namespace CussacTarot.Core.Domains
{
    public class ChooseLaunchGameService : ILaunchGameService
    {
        private readonly ILaunchGameService<Gamer> _LaunchFirstGameService;
        private readonly ILaunchGameService<ScoreByGamer> _LaunchSecondGameService;


        public ChooseLaunchGameService(ILaunchGameService<Gamer> launchFirstGameService, ILaunchGameService<ScoreByGamer> launchSecondGameService)
        {
            _LaunchFirstGameService = launchFirstGameService ?? throw new ArgumentNullException(nameof(launchFirstGameService));
            _LaunchSecondGameService = launchSecondGameService ?? throw new ArgumentNullException(nameof(launchSecondGameService));
        }


        public void Launch(IEnumerable<object> list)
        {
            if (list == null || !list.Any())
            {
                return;
            }

            IEnumerable<Gamer> gamers = list.OfType<Gamer>();
            if (gamers != null && gamers.Any())
            {
                _LaunchFirstGameService.Launch(gamers);
                return;
            }


            IEnumerable<ScoreByGamer> scoreByGamers = list.OfType<ScoreByGamer>();
            if (scoreByGamers != null && scoreByGamers.Any())
            {
                _LaunchSecondGameService.Launch(scoreByGamers);
                return;
            }
        }
    }
}
