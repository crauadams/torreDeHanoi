using HanoiEntity;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HanoiService
{
    public static class NetworkService
    {
        private static string _id = "160834101376.161631451733";
        private static string _secret = "73fb1e1a0061596d1551dd54c645f77b";
        private static string _auth = $"Authorization: Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_id}:{_secret}"))}";

        private async static Task PostJsonAsync(string url, string json)
        {
            try
            {
                using (var client = new WebClient())
                {

                    client.Proxy = WebRequest.DefaultWebProxy;
                    client.Headers.Add("Content-Type", "application/json");
                    client.Headers.Add(_auth);

                    await client.UploadDataTaskAsync(url, "POST", Encoding.UTF8.GetBytes(json));
                }
            }
            catch (WebException ex)
            {
                var resp = new StreamReader(ex.Response?.GetResponseStream()).ReadToEnd();
                throw new Exception(resp);
            }
            catch (Exception ex)
            {
            }
        }

        public async static Task PostSlackMessage(string msg)
        {
            var url = "https://hooks.slack.com/services/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";
            await PostJsonAsync(url, JsonConvert.SerializeObject(new SlackMessage(msg)));
        }
    }
}
