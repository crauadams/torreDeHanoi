using HanoiService;
using System.Net.Http;
using System.Web.Http;

namespace HanoiAPI.Controllers
{
    public class SlackApiController : BaseApiController
    {
        private SlackService slackService;

        public SlackApiController(SlackService slackService)
        {
            this.slackService = slackService;
        }

        [HttpPost]
        [Route("api/integration/slack/historico")]
        public HttpResponseMessage UploadSlack()
        {
            return ProcessResponse(() => {
                slackService.EnviarHistorico();
            });
        }
    }
}