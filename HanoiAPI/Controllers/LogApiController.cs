using HanoiService;
using System.Net.Http;
using System.Web.Http;

namespace HanoiAPI.Controllers
{
    public class LogApiController : BaseApiController
    {
        private LogService logService;

        public LogApiController(LogService logService)
        {
            this.logService = logService;
        }

        [HttpGet]
        [Route("api/log/movimento/{gameId}")]
        public HttpResponseMessage GetLogMovimento(int gameId)
        {
            return ProcessResponse(() => {
                return logService.GetLogMovimentacao(gameId);
            });
        }

        [HttpGet]
        [Route("api/log/historico")]
        public HttpResponseMessage GetLogHistorico()
        {
            return ProcessResponse(() => {
                return logService.GetLogHistorico();
            });
        }
    }
}