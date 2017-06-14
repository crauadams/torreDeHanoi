using HanoiAPI.SignalR;
using HanoiService;
using Microsoft.AspNet.SignalR;
using System.Net.Http;
using System.Web.Http;

namespace HanoiAPI.Controllers
{
    public class GameApiController : BaseApiController
    {
        private GameService gameService;
        private IHubContext gameHub = GlobalHost.ConnectionManager.GetHubContext<GameHub>();

        public GameApiController(GameService gameService)
        {
            this.gameService = gameService;
        }

        [HttpPost]
        [Route("api/game/start-game/{quantidadeDeDiscos}")]
        public HttpResponseMessage StartGame(int quantidadeDeDiscos)
        {
            return ProcessResponse(() => {
                return gameService.IniciarGame(quantidadeDeDiscos);
            });
        }

        [HttpPut]
        [Route("api/game/mover-disco/game-id/{gameId}")]
        public HttpResponseMessage MoverDisco(int gameId)
        {
            return ProcessResponse(() => {
                gameService.MovimentarDisco(gameId);
            });
        }
    }
}
