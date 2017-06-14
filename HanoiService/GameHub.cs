using Microsoft.AspNet.SignalR;
using System.Diagnostics;
using System.Threading.Tasks;

namespace HanoiAPI.SignalR
{
    public class GameHub : Hub
    {
        private IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<GameHub>();

        public override Task OnConnected()
        {
            Debug.WriteLine("Cliente conectado...");
            return base.OnConnected();
        }
    }
}