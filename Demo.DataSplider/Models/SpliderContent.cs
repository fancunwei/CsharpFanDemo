using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.DataSplider.Models
{
    /// <summary>
    /// 采集内容表
    /// </summary>
    public class SpliderContent
    {
        public string Id { get; set; }
        /// <summary>
        /// 对应的规则Id
        /// </summary>
        public string SpliderRuleId { get; set; }

        public List<Field> Fields { get; set; }

    }
}
