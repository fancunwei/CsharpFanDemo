using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.DataSplider.Models
{
    /// <summary>
    /// 采集规则-能满足列表页/详情页。
    /// </summary>
    public class SpliderRule
    {
        public string Id { get; set; }

        public string Url { get; set; }
        /// <summary>
        /// 网页块
        /// </summary>
        public string ContentXPath { get; set; }
        /// <summary>
        /// 支持列表式
        /// </summary>
        public string EachXPath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<RuleField> RuleFields { get; set; }
    }
}
