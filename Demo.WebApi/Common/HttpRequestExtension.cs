using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApi.Common
{
    public static class HttpRequestExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static async Task<string> GetRawBodyStringAsyn(this HttpRequest httpRequest, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            using (StreamReader reader = new StreamReader(httpRequest.Body, encoding))
            {
                return await reader.ReadToEndAsync();
            }
        }
        /// <summary>
        /// 二进制
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static async Task<byte[]> GetRawBodyBinaryAsyn(this HttpRequest httpRequest, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            using (StreamReader reader = new StreamReader(httpRequest.Body, encoding))
            {
                using (var ms = new MemoryStream(2048))
                {
                    await httpRequest.Body.CopyToAsync(ms);
                    return ms.ToArray();  // returns base64 encoded string JSON result
                }
            }
        }
    }
}
