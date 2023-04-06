using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CussacTarot.Core.Repositories;
using CussacTarot.Models;

namespace CussacTarot.Core.Domains
{
    public class LaunchGameService
    {
        public const int NUMBER_GAMER_BY_TABLE_FOUR = 4;
        private const int NUMBER_GAMER_BY_TABLE_FIVE = 5;

        private readonly IRepository<int, GameSheet> _GameSheetRepository;

        protected LaunchGameService(IRepository<int, GameSheet> gameSheetRepository)
        {
            _GameSheetRepository = gameSheetRepository ?? throw new ArgumentNullException(nameof(gameSheetRepository));
        }

        protected void CreateGamesSheets(IEnumerable<Gamer> gamers)
        {
            int numberGamer = gamers.Count();
            int numberTableFour = numberGamer / NUMBER_GAMER_BY_TABLE_FOUR - numberGamer % NUMBER_GAMER_BY_TABLE_FOUR;
            int numberTableFive = numberGamer % NUMBER_GAMER_BY_TABLE_FOUR;

            for (int gameWithTableFour = 0; gameWithTableFour < numberTableFour; gameWithTableFour++)
            {
                gamers = gamers.Take(NUMBER_GAMER_BY_TABLE_FOUR);
                GameSheet gameSheet = new()
                {
                    Created = DateTime.Now,
                    Scores = gamers.Select(
                        g => new ScoreByGamer()
                        {
                            Gamer = g,
                            GamerId = g.Id
                        }).ToList()
                };
                _GameSheetRepository.AddOrUpdate(gameSheet);
                gamers = gamers.Skip(NUMBER_GAMER_BY_TABLE_FOUR).ToList();
            }

            for (int gameWithTableFive = 0; gameWithTableFive < numberTableFive; gameWithTableFive++)
            {
                gamers = gamers.Take(NUMBER_GAMER_BY_TABLE_FIVE);
                GameSheet gameSheet = new()
                {
                    Created = DateTime.Now,
                    Scores = gamers.Select(
                        g => new ScoreByGamer()
                        {
                            Gamer = g,
                            GamerId = g.Id
                        }).ToList()
                };
                _GameSheetRepository.AddOrUpdate(gameSheet);
                gamers = gamers.Skip(NUMBER_GAMER_BY_TABLE_FIVE).ToList();
            }            
        }
    }
}
