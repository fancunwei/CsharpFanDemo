using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Demo.DataSplider.Models
{
    /// <summary>
    /// 支持列表和详情页
    /// </summary>
    public class ArticleSplider : IDataSplider
    {
        /// <summary>
        /// 根据Rule
        /// </summary>
        /// <param name="rule"></param>
        /// <returns></returns>
        public List<SpliderContent> GetByRule(SpliderRule rule)
        {
            var url = rule.Url;
            HtmlWeb web = new HtmlWeb();
            //1.支持从web或本地path加载html
            var htmlDoc = web.Load(url);
            var contentnode = htmlDoc.DocumentNode.SelectSingleNode(rule.ContentXPath);

            var list = new List<SpliderContent>();
            //列表页
            if (!string.IsNullOrWhiteSpace(rule.EachXPath))
            {
                var itemsNodes = contentnode.SelectNodes(rule.EachXPath);
                foreach (var item in itemsNodes)
                {
                    var fields = GetFields(item, rule);
                    list.Add(new SpliderContent()
                    {
                        Fields = fields,
                        SpliderRuleId = rule.Id
                    });
                }
                return list;
            }
            //详情页
            var cfields = GetFields(contentnode, rule);
            list.Add(new SpliderContent()
            {
                Fields = cfields,
                SpliderRuleId = rule.Id
            });
            return list;
        }

        public List<Field> GetFields(HtmlNode item, SpliderRule rule)
        {
            var fields = new List<Field>();

            foreach (var rulefield in rule.RuleFields)
            {
                var field = new Field() { DisplayName = rulefield.DisplayName, FieldName = "" };

                var fieldnode = item.SelectSingleNode(rulefield.XPath);
                if (fieldnode != null)
                {

                    field.InnerHtml = fieldnode.InnerHtml;
                    field.InnerText = fieldnode.InnerText;
                    field.AfterRegexHtml = !string.IsNullOrWhiteSpace(rulefield.InnerHtmlRegex) ? Regex.Replace(fieldnode.InnerHtml, rulefield.InnerHtmlRegex, "") : fieldnode.InnerHtml;
                    field.AfterRegexText = !string.IsNullOrWhiteSpace(rulefield.InnerTextRegex) ? Regex.Replace(fieldnode.InnerText, rulefield.InnerTextRegex, "") : fieldnode.InnerText;

                    //field.AfterRegexHtml = Regex.Replace(fieldnode.InnerHtml, rulefield.InnerHtmlRegex, "");
                    //field.AfterRegexText = Regex.Replace(fieldnode.InnerText, rulefield.InnerTextRegex, "");
                    if (!string.IsNullOrWhiteSpace(rulefield.Attribute))
                    {
                        field.Value = fieldnode.Attributes[rulefield.Attribute].Value;
                    }
                    else
                    {
                        field.Value = rulefield.IsFirstInnerText ? field.AfterRegexText : field.AfterRegexHtml;
                    }
                    }
                fields.Add(field);
            }
            return fields;
        }
    }
}
