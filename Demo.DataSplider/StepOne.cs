using Demo.DataSplider.Models;
using HtmlAgilityPack;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Demo.DataSplider
{
    public class StepOne
    {
        /// <summary>
        /// 解析
        /// </summary>
        /// <returns></returns>
        public List<Article> ParseCnBlogs()
        {
            var url = "https://www.cnblogs.com";
            HtmlWeb web = new HtmlWeb();
            //1.支持从web或本地path加载html
            var htmlDoc = web.Load(url);
            var post_listnode = htmlDoc.DocumentNode.SelectSingleNode("//div[@id='post_list']");
            //Console.WriteLine("Node Name: " + post_listnode.Name + "\n" + post_listnode.OuterHtml);

            var postitemsNodes = post_listnode.SelectNodes("div[@class='post_item']");
            var articles = new List<Article>();
            var digitRegex = @"[^0-9]+";
            foreach (var item in postitemsNodes)
            {
                var article = new Article();
                var diggnumnode = item.SelectSingleNode("*//span[@class='diggnum']");
                //body
                var post_item_bodynode = item.SelectSingleNode("div[@class='post_item_body']");
                //写法一
                //var titlenode = post_item_bodynode.SelectSingleNode("*//a[@class='titlelnk']");
                //写法二
                var titlenode = post_item_bodynode.SelectSingleNode(post_item_bodynode.XPath+"//a[@class='titlelnk']");
                var summarynode = post_item_bodynode.SelectSingleNode("p[@class='post_item_summary']");
                //foot
                var footnode = post_item_bodynode.SelectSingleNode("div[@class='post_item_foot']");
                var authornode = footnode.ChildNodes[1];
                var commentnode = footnode.SelectSingleNode("span[@class='article_comment']");
                var viewnode = footnode.SelectSingleNode("span[@class='article_view']");


                article.Diggit = int.Parse(diggnumnode.InnerText);
                article.Title = titlenode.InnerText;
                article.Url = titlenode.Attributes["href"].Value;
                article.Summary = titlenode.InnerHtml;
                article.Author = authornode.InnerText;
                article.AuthorUrl = authornode.Attributes["href"].Value;

                article.Comment = int.Parse(Regex.Replace(commentnode.ChildNodes[0].InnerText, digitRegex, ""));
                article.View = int.Parse(Regex.Replace(viewnode.ChildNodes[0].InnerText, digitRegex, ""));

                articles.Add(article);
            }
            return articles;
        }



    }
}
