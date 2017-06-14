using System;
using System.Text;
using System.Threading.Tasks;

namespace HanoiService
{
    public class SlackService
    {
        private LogService logService;

        public SlackService(LogService logService)
        {
            this.logService = logService;
        }

        public void EnviarHistorico()
        {
            var sb = new StringBuilder();
            logService.GetLogHistorico().ForEach(x => sb.AppendLine(x.ToString()));
            var msg = $"{DateTime.Now.ToString("HH:mm:ss")} | {sb.ToString()}";
            Task.Factory.StartNew(async ()=> { await NetworkService.PostSlackMessage(msg); });
        }
    }
}
