
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using zipkin4net.Transport.Http;

namespace Demo.ZipkinCommon
{
    public class HTTPHelper : ControllerBase
    {
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="url"></param>
        /// <param name="keyValues"></param>
        /// <param name="timeout"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static async Task<string> GetAsync(string url, Dictionary<string, string> keyValues, int timeout = 0, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            var appName = ConfigureSettings.AppSettingConfig["applicationName"];

            using (var httpClient = new HttpClient(new TracingHandler(appName)))
            {
                var response = await httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    return response.ReasonPhrase;
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return content;
                }
            }
        }
    }
}
