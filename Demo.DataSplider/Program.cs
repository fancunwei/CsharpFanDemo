using Demo.DataSplider.DemoPick;
using System;

namespace Demo.DataSplider
{
    class Program
    {
        static void Main(string[] args)
        {

            //演示HtmlAgilityPack   Parse用法
            //var parse = new HtmlParse();
            //parse.FromFile();
            //parse.FromString();
            //parse.FromWeb();

            //演示从博客园获取信息
            var one = new StepOne();
            var list = one.ParseCnBlogs();
            foreach (var i in list)
            {
                Console.WriteLine($"作者：{i.Author},     推荐数：{i.Diggit},   评论数：{i.Comment}, 阅读数：{i.View},   标题:{i.Title}");
            }

            Console.ReadKey();


        }
    }
}
