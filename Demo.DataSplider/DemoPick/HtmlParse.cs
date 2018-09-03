using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.DataSplider.DemoPick
{
    public class HtmlParse
    {
        /// <summary>
        /// 从文件读取
        /// </summary>
        public void FromFile()
        {
            var path = @"test.html";
            var doc = new HtmlDocument();
            doc.Load(path);
            var node = doc.DocumentNode.SelectSingleNode("//body");
            Console.WriteLine(node.OuterHtml);
        }
        /// <summary>
        /// 从字符串读取
        /// </summary>
        public void FromString()
        {
            var html = @"<!DOCTYPE html>
<html>
<body>
	<h1>This is <b>bold</b> heading</h1>
	<p>This is <u>underlined</u> paragraph</p>
	<h2>This is <i>italic</i> heading</h2>
</body>
</html> ";

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var htmlBody = htmlDoc.DocumentNode.SelectSingleNode("//body");

            Console.WriteLine(htmlBody.OuterHtml);
        }
        /// <summary>
        /// 从网络地址加载
        /// </summary>
        public void FromWeb() {
            var html = @"https://www.cnblogs.com/";

            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(html);

            var node = htmlDoc.DocumentNode.SelectSingleNode("//div[@id='post_list']");

            Console.WriteLine("Node Name: " + node.Name + "\n" + node.OuterHtml);
        }
    }
}
