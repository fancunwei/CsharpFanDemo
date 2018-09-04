using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.DataSplider.Models
{
    /// <summary>
    /// 采集规则接口
    /// </summary>
    public interface IDataSplider
    {
        /// <summary>
        /// 得到内容
        /// </summary>
        /// <param name="rule"></param>
        /// <returns></returns>
        List<SpliderContent> GetByRule(SpliderRule rule);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="rule"></param>
        /// <returns></returns>
        List<Field> GetFields(HtmlNode node, SpliderRule rule);
    }
}
