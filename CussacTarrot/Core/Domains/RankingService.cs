using CussacTarot.Core.Repositories;
using CussacTarot.Models;

namespace CussacTarot.Core.Domains
{
    public class RankingService
    {
        private readonly IRepository<int, GameSheet> _GameSheetRepository;

        public RankingService(IRepository<int, GameSheet> gameSheetRepository)
        {
            _GameSheetRepository = gameSheetRepository ?? throw new ArgumentNullException(nameof(gameSheetRepository));
        }

        public IEnumerable<RankingByPlayer> GetRankingByPlayer(DateTime? dateSearch)
        {
            List<RankingPlayer> rankingPlayers = new();

            IEnumerable<GameSheet> gamesSheets = _GameSheetRepository.GetAll();
            if (dateSearch.HasValue)
            {
                gamesSheets = gamesSheets.Where(g => g.PeriodGame.End.HasValue && g.PeriodGame.End.Value.Date == dateSearch.Value.Date);
            }                
                
            foreach(GameSheet gameSheet in gamesSheets)
            {
                foreach(ScoreByGamer scoreByGamer in gameSheet.Scores)
                {
                    RankingPlayer rankingPlayer = new RankingPlayer()
                    {
                        PeriodGame = gameSheet.PeriodGame,
                        Gamer = scoreByGamer.Gamer,
                        Score = scoreByGamer.Score
                    };
                    rankingPlayers.Add(rankingPlayer);
                }                
            }


            return rankingPlayers.GroupBy(e => e.Gamer).Select(e => new RankingByPlayer()
            {
                IdGamer = e.Key.Id,
                NameSurname = $"{e.Key.Name} {e.Key.Surname}" ,
                GameScore = e.Select(a => new GameScore()
                {
                    PeriodGame = a.PeriodGame,
                    Score = a.Score
                })
            }); ;
        }

    }
}
