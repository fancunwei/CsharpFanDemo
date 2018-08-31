using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApi.Common
{
    /// <summary>
    /// 格式化程序
    /// </summary>
    public class RawRequestBodyFormatter : IInputFormatter
    {
        public RawRequestBodyFormatter()
        {

        }

        public bool CanRead(InputFormatterContext context)
        {
            if (context == null) throw new ArgumentNullException("argument is Null");
            var contentType = context.HttpContext.Request.ContentType;
            if (string.IsNullOrEmpty(contentType) || contentType == "text/plain" || contentType == "application/octet-stream")
                return true;
            return false;
        }

        public async Task<InputFormatterResult> ReadAsync(InputFormatterContext context)
        {
            var request = context.HttpContext.Request;
            var contentType = context.HttpContext.Request.ContentType;
            if (string.IsNullOrEmpty(contentType) || contentType.ToLower() == "text/plain")
            {
                using (StreamReader reader = new StreamReader(request.Body, Encoding.UTF8))
                {
                    var content = await reader.ReadToEndAsync();
                    return await InputFormatterResult.SuccessAsync(content);
                }
            }
            if (contentType == "application/octet-stream")
            {
                using (StreamReader reader = new StreamReader(request.Body, Encoding.UTF8))
                {
                    using (var ms = new MemoryStream(2048))
                    {
                        await request.Body.CopyToAsync(ms);
                        var content = ms.ToArray();

                        return await InputFormatterResult.SuccessAsync(content);
                    }
                }
            }
            return await InputFormatterResult.FailureAsync();
        }
    }
}
