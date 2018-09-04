using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.DataSplider.Models
{
    public class Field
    {
        public string Id { get; set; }

        public string DisplayName { get; set; }

        public string FieldName { get; set; }


        /// <summary>
        /// 过滤后的值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 获取的HTML
        /// </summary>
        public string InnerHtml { get; set; }
        /// <summary>
        /// 获取的Text
        /// </summary>
        public string InnerText { get; set; }
        /// <summary>
        /// 过滤后html
        /// </summary>
        public string AfterRegexHtml { get; set; }
        /// <summary>
        /// 过滤后
        /// </summary>
        public string AfterRegexText { get; set; }
    }
}
