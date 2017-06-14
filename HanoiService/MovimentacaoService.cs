using HanoiAPI.SignalR;
using HanoiEntity;
using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using System.Linq;
using System;

namespace HanoiService
{
    public class MovimentacaoService
    {
        private int qtdMovimentacoesDoDisco;
        private Queue<Movimento> movimentos;
        private LogService logService;

        public MovimentacaoService(LogService logService)
        {
            if (logService == null)
                throw new ArgumentNullException("Parametro não pode ser null.");

            this.logService = logService;
            this.movimentos = new Queue<Movimento>();
        }

        public void CalcularMovimento(Game game, int disco, Torre origem, Torre destino, Torre torreTemporaria)
        {
            if (game == null || origem == null || destino == null || torreTemporaria == null)
                throw new ArgumentNullException("Parametros não podem ser null");

            if (disco == 0 && origem.Discos.Any())
            {
                ProcessarMovimentacao(game, origem.Discos.ElementAt(0).Id, origem, destino);
                destino.Discos.Insert(0, origem.Discos.ElementAt(0));
                origem.Discos.RemoveAt(0);
            }
            else
            {
                if (!game.IsFinalDoJogo())
                {
                    CalcularMovimento(game, disco - 1, origem, torreTemporaria, destino);

                    var dId = origem.Discos.Any() ? origem.Discos.ElementAt(0).Id : torreTemporaria.Discos.ElementAt(0).Id;

                    ProcessarMovimentacao(game, dId, origem, destino);

                    if (origem.Discos.Any())
                    {
                        destino.Discos.Insert(0, origem.Discos.ElementAt(0));
                        origem.Discos.RemoveAt(0);
                    }

                    CalcularMovimento(game, disco - 1, torreTemporaria, destino, origem);
                }
            }
        }

        public void ExecutarMovimento()
        {
            if (movimentos.Any())
            {
                qtdMovimentacoesDoDisco++;

                var movimento = movimentos.Dequeue();

                GlobalHost.ConnectionManager.GetHubContext<GameHub>().Clients.All.moverDisco(movimento, qtdMovimentacoesDoDisco);
            }
        }

        private void ProcessarMovimentacao(Game game, int disco, Torre origem, Torre destino)
        {
            var isValid = (!destino.Discos.Any() || destino.Discos.First().Id > disco) &&
                (!game.IsFinalDoJogo());

            if (isValid)
            {
                ArmazenarMovimento(game.Id, disco, origem.Id, destino.Id);
                logService.CriarLogMovimento(game.Id, disco, origem.Id, destino.Id);
                game.QuantidadeDeMovimentosExecutados++;
            }

            if (game.IsFinalDoJogo())
                logService.FinalizarHistorico(game.Id);
        }

        private void ArmazenarMovimento(int gameId, int disco, int origem, int destino)
        {
            var movimento = new Movimento
            {
                GameId = gameId,
                DiscoId = disco,
                OrigemId = origem,
                DestinoId = destino
            };

            movimentos.Enqueue(movimento);
        }
    }
}
