using HanoiEntity;
using HanoiRepository;
using HanoiService;

namespace HanoiUnitTest
{
    public class BaseUnitTest
    {
        public IEntityRepository<Game> gameRep { get; set; }
        public IEntityRepository<MovimentoLog> movimentoRep { get; set; }
        public IEntityRepository<HistoricoLog> historicoRep { get; set; }
        public LogService logService { get; set; }
        public MovimentacaoService movimentoService { get; set; }
        public GameService gameService { get; set; }

        public BaseUnitTest()
        {
            gameRep = new EntityRepository<Game>();
            movimentoRep = new EntityRepository<MovimentoLog>();
            historicoRep = new EntityRepository<HistoricoLog>();
            logService = new LogService(movimentoRep, historicoRep);
            movimentoService = new MovimentacaoService(logService);
            gameService = new GameService(gameRep, logService, movimentoService);
        }

        public GameService BuildGameService()
        {
            return new GameService(gameRep, logService, movimentoService);
        }
    }
}
