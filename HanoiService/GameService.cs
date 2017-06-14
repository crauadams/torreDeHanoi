using System;
using HanoiEntity;
using HanoiRepository;
using System.Linq;

namespace HanoiService
{
    public class GameService
    {
        #region | Properties

        private IEntityRepository<Game> gameRepository;
        private LogService logService;
        private MovimentacaoService movimentacaoService;
        private System.Timers.Timer timer;

        #endregion

        #region | Constructor

        public GameService(
            IEntityRepository<Game> gameRepository,
            LogService logService,
            MovimentacaoService movimentacaoService
            )
        {
            if (gameRepository == null || logService == null || movimentacaoService == null)
                throw new ArgumentNullException("Parametros não podem ser nulos");

            this.gameRepository = gameRepository;
            this.logService = logService;
            this.movimentacaoService = movimentacaoService;

            timer = new System.Timers.Timer();
            timer.Elapsed += ExecutarMovimento;
            timer.Interval = 1000;
            timer.Enabled = true;
            timer.Start();
        }

        #endregion

        #region | Methods

        public Game IniciarGame(int quantidadeDeDiscos)
        {
            if (quantidadeDeDiscos <= 0)
                throw new ArgumentException("Quantidade de discos deve ser maior que zero.");

            var game = new Game($"Jogo {gameRepository.Count()}", quantidadeDeDiscos);

            gameRepository.Add(game);

            logService.Add(new HistoricoLog
            {
                DataHoraChamada = DateTime.Now,
                Game = game
            });

            return game;
        }

        public void MovimentarDisco(int gameId)
        {
            var game = gameRepository.Get(gameId);

            if (game == null)
                throw new ArgumentException("Game não encontrado.");

            var torreOrigem = game.Torres.ElementAt(0);
            var torreDestino = game.Torres.ElementAt(2);
            var torreTemporaria = game.Torres.ElementAt(1);

            movimentacaoService.CalcularMovimento(game, torreOrigem.Discos.Count, torreOrigem, torreDestino, torreTemporaria);
        }

        private void ExecutarMovimento(object sender, System.Timers.ElapsedEventArgs e)
        {
            movimentacaoService.ExecutarMovimento();
        }

        #endregion
    }
}
