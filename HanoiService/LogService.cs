using HanoiEntity;
using HanoiRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HanoiService
{
    public class LogService
    {
        private IEntityRepository<MovimentoLog> movimentoLogRepository;
        private IEntityRepository<HistoricoLog> historicoLogRepository;

        public LogService(
            IEntityRepository<MovimentoLog> movimentoLogRepository,
            IEntityRepository<HistoricoLog> historicoLogRepository
            )
        {
            if (movimentoLogRepository == null || historicoLogRepository == null)
                throw new ArgumentNullException("Parametros não podem ser null.");

            this.movimentoLogRepository = movimentoLogRepository;
            this.historicoLogRepository = historicoLogRepository;
        }

        public void Add(HistoricoLog log)
        {
            if (log != null)
                historicoLogRepository.Add(log);
        }

        public List<MovimentoLog> GetLogMovimentacao(int gameId)
        {
            return movimentoLogRepository.Get(x => x.Movimento.GameId == gameId).ToList();
        }

        public List<HistoricoDTO> GetLogHistorico()
        {
            return historicoLogRepository.GetAll().ToList().ConvertAll<HistoricoDTO>(x => x);
        }

        public void CriarLogMovimento(int gameId, int disco, int origem, int destino)
        {
            movimentoLogRepository.Add(new MovimentoLog
            {
                Movimento = new Movimento
                {
                    GameId = gameId,
                    DiscoId = disco,
                    OrigemId = origem,
                    DestinoId = destino
                },
                Data = DateTime.Now
            });
        }

        public void FinalizarHistorico(int gameId)
        {
            var historico = historicoLogRepository.Get(x => x.Game.Id == gameId).FirstOrDefault();

            if (historico != null)
                historico.DataHoraFinalizacao = DateTime.Now;
        }
    }
}
