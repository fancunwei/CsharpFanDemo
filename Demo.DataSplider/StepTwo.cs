using Demo.DataSplider.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.DataSplider
{
    /// <summary>
    /// 自定义规则获取内容
    /// </summary>
    public class StepTwo
    {
        /// <summary>
        /// 
        /// </summary>
        public void RunArticleRule()
        {
            var postitembodyXPath = "div[@class='post_item_body']//";
            var postitembodyFootXPath = postitembodyXPath+ "div[@class='post_item_foot']//";
            var rule = new SpliderRule()
            {
                ContentXPath = "//div[@id='post_list']",
                EachXPath = "div[@class='post_item']",
                Url = "https://www.cnblogs.com",
                RuleFields = new List<RuleField>() {
                         new RuleField(){ DisplayName="推荐", XPath="*//span[@class='diggnum']", IsFirstInnerText=true },
                         new RuleField(){ DisplayName="标题",XPath=postitembodyXPath+"a[@class='titlelnk']", IsFirstInnerText=true },
                         new RuleField(){ DisplayName="URL",XPath=postitembodyXPath+"a[@class='titlelnk']",Attribute="href", IsFirstInnerText=true },
                         new RuleField(){ DisplayName="简要",XPath=postitembodyXPath+"p[@class='post_item_summary']", IsFirstInnerText=true },
                         new RuleField(){ DisplayName="作者",XPath=postitembodyFootXPath+"a[@class='lightblue']", IsFirstInnerText=true },
                         new RuleField(){ DisplayName="作者URL",XPath=postitembodyFootXPath+"a[@class='lightblue']",Attribute="href", IsFirstInnerText=true },
                         new RuleField(){ DisplayName="讨论数", XPath="span[@class='article_comment']",IsFirstInnerText=true, InnerTextRegex=@"[^0-9]+"  },
                         new RuleField(){ DisplayName="阅读数", XPath=postitembodyFootXPath+"span[@class='article_view']",IsFirstInnerText=true, InnerTextRegex=@"[^0-9]+"  },
                    }
            };
            var splider = new ArticleSplider();
            var list = splider.GetByRule(rule);
            foreach (var item in list)
            {
                var msg = string.Empty;
                item.Fields.ForEach(M =>
                {
                    if (M.DisplayName != "简要" && !M.DisplayName.Contains("URL"))
                    {
                        msg += $"{M.DisplayName}:{M.Value}";
                    }
                });
                Console.WriteLine(msg);
            }
        }

        /// <summary>
        /// 详情
        /// </summary>
        public void RunArticleDetail() {
           

            var rule = new SpliderRule()
            {
                ContentXPath = "//div[@id='post_detail']",
                EachXPath = "",
                Url = " https://www.cnblogs.com/fancunwei/p/9581168.html",
                RuleFields = new List<RuleField>() {
                         new RuleField(){ DisplayName="标题",XPath="*//div[@class='post']//a[@id='cb_post_title_url']", IsFirstInnerText=true },
                         new RuleField(){ DisplayName="详情",XPath="*//div[@class='postBody']//div[@class='blogpost-body']",Attribute="", IsFirstInnerText=false }
                           }
            };
            var splider = new ArticleSplider();
            var list = splider.GetByRule(rule);
            foreach (var item in list)
            {
                var msg = string.Empty;
                item.Fields.ForEach(M =>
                {
                    Console.WriteLine($"{M.DisplayName}:{M.Value}");
                });
                Console.WriteLine(msg);
            }
        }

        /// <summary>
        /// 天气预报
        /// </summary>
        public void RunWeather() {

            var rule = new SpliderRule()
            {
                ContentXPath = "//div[@id='15d']",
                EachXPath = "*//li",
                Url = "http://www.weather.com.cn/weather15d/101020100.shtml",
                RuleFields = new List<RuleField>() {
                         new RuleField(){ DisplayName="日期",XPath="span[@class='time']", IsFirstInnerText=true },
                         new RuleField(){ DisplayName="天气",XPath="span[@class='wea']",Attribute="", IsFirstInnerText=false },
                         new RuleField(){ DisplayName="区间",XPath="span[@class='tem']",Attribute="", IsFirstInnerText=false },
                         new RuleField(){ DisplayName="风向",XPath="span[@class='wind']",Attribute="", IsFirstInnerText=false },
                         new RuleField(){ DisplayName="风力",XPath="span[@class='wind1']",Attribute="", IsFirstInnerText=false },
                           }
            };
            var splider = new ArticleSplider();
            var list = splider.GetByRule(rule);
            foreach (var item in list)
            {
                var msg = string.Empty;
                item.Fields.ForEach(M =>
                {
                        msg += $"{M.DisplayName}:{M.Value} ";
                });
                Console.WriteLine(msg);
            }

        }
    }
}
