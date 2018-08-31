using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.QueueDemo
{
    /// <summary>
    /// 消息体
    /// </summary>
    public class DemoMessage
    {
        public string BusinessType { get; set; }
        public string BusinessId { get; set; }
        public string Body { get; set; }
    }
}
