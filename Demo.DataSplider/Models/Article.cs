using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.DataSplider.Models
{
    public class Article
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 概要
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// 文章链接
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 推荐数
        /// </summary>
        public long Diggit { get; set; }
        /// <summary>
        /// 评论数
        /// </summary>
        public long Comment { get; set; }
        /// <summary>
        /// 阅读数
        /// </summary>
        public long View { get; set; }
        /// <summary>
        ///明细
        /// </summary>
        public string Detail { get; set; }
        /// <summary>
        ///作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 作者链接
        /// </summary>
        public string AuthorUrl { get; set; }
    }
}
