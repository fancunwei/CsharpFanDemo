using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Demo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get() {
            return "ok";
        }

        [HttpPost]
        [Route("PostX")]
        public ActionResult<string> Post([FromBody]string value)
        {
            return value;
        }
        /// <summary>
        /// 接收text/plain
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("PostText")]
        public async Task<string> PostText()
        {
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                return await reader.ReadToEndAsync();
            }
        }
        /// <summary>
        /// 接收
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("PostBinary")]
        public async Task<byte[]> PostBinary()
        {
            using (var ms = new MemoryStream(2048))
            {
                await Request.Body.CopyToAsync(ms);
                return ms.ToArray();  // returns base64 encoded string JSON result
            }
        }

        [HttpPost]
        [Route("PostTextX")]
        public async Task<string> PostTextX()
        {
            return await Request.GetRawBodyStringAsyn();
        }
        /// <summary>
        /// 接收
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("PostBinaryX")]
        public async Task<byte[]> PostBinaryX()
        {
            return await Request.GetRawBodyBinaryAsyn();
        }
        /// <summary>
        /// 接收text/plain
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("PostTextPlus")]
        public string PostTextPlus([FromBody] string value)
        {
            return value;
        }
        /// <summary>
        /// 接收
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("PostBinaryPlus")]
        public byte[] PostBinaryPlus([FromBody] byte[] value)
        {
            return value;
        }


    }
}
