using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.DataSplider.Models
{
    /// <summary>
    /// 自定义属性字段
    /// </summary>
    public class RuleField
    {
        public string Id { get; set; }

        public string DisplayName { get; set; }
        /// <summary>
        /// 用于存储的别名
        /// </summary>
        public string FieldName { get; set; }
        public string XPath { get; set; }
        public string Attribute { get; set; }
        /// <summary>
        /// 针对获取的HTml正则过滤
        /// </summary>
        public string InnerHtmlRegex { get; set; }
        /// <summary>
        /// 针对获取的Text正则过滤
        /// </summary>
        public string InnerTextRegex { get; set; }
        /// <summary>
        /// 是否优先取InnerText
        /// </summary>
        public bool IsFirstInnerText { get; set; }

    }
}
