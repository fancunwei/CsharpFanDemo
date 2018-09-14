using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FanQuick.Repository
{
    public interface IRepository<TDocument> where TDocument:EntityBase
    {
        IQueryable<TDocument> Queryable { get; }
        bool Any(Expression<Func<TDocument, bool>> filter);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        bool Delete(Expression<Func<TDocument, bool>> filter);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IEnumerable<TDocument> Find(Expression<Func<TDocument, bool>> filter);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="document"></param>
        void Insert(TDocument document);
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="documents"></param>
        void Insert(IEnumerable<TDocument> documents);
        /// <summary>
        /// 统计。
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        long Count(Expression<Func<TDocument, bool>> filter);

        TDocument FindOneAndDelete(Expression<Func<TDocument, bool>> filter);

        TDocument FindOneAndUpdate(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update);
    }
}
