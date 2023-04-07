using CussacTarot.Models;

namespace CussacTarot.Core.Domains
{
    public class ChooseLaunchGameService : ILaunchGameService
    {
        private readonly ILaunchGameService<Gamer> _LaunchFirstGameService;
        private readonly ILaunchGameService<GameSheet> _LaunchSecondGameService;


        public ChooseLaunchGameService(ILaunchGameService<Gamer> launchFirstGameService, ILaunchGameService<GameSheet> launchSecondGameService)
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


            IEnumerable<GameSheet> gameSheets = list.OfType<GameSheet>();
            if (gameSheets != null && gameSheets.Any())
            {
                _LaunchSecondGameService.Launch(gameSheets);
                return;
            }
        }
    }
}
