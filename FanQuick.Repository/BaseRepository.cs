using FanQuick.Config;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace FanQuick.Repository
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="EntityBase"></typeparam>
    public class BaseRepository<TDocument> : IRepository<TDocument> where TDocument : EntityBase
    {
        private static readonly string connstr = ConfigEx.mongodbAddr;
        private const string dbName = ConfigEx.mongodbName;
        private static readonly MongoClient mongoClient = new MongoClient(connstr);

        private IMongoCollection<TDocument> collection => mongoClient.GetDatabase(dbName).GetCollection<TDocument>(typeof(TDocument).Name);

        public IQueryable<TDocument> Queryable => collection.AsQueryable();

        public bool Any(Expression<Func<TDocument, bool>> filter) {
            return collection.Find(filter).Any();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public bool Delete(Expression<Func<TDocument, bool>> filter)
        {
            return collection.DeleteMany(filter).IsAcknowledged;
            // throw new NotImplementedException();
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IEnumerable<TDocument> Find(Expression<Func<TDocument, bool>> filter)
        {
            return collection.Find(filter).ToList();
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="document"></param>
        public void Insert(TDocument document)
        {
            collection.InsertOne(document);
        }
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="documents"></param>
        public void Insert(IEnumerable<TDocument> documents)
        {
            collection.InsertMany(documents);
        }
        /// <summary>
        /// 统计。
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public long Count(Expression<Func<TDocument, bool>> filter)
        {
            return Queryable.LongCount(filter);
        }

        public TDocument FindOneAndDelete(Expression<Func<TDocument, bool>> filter)
        {
            return collection.FindOneAndDelete(filter);
        }

        public TDocument FindOneAndUpdate(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update)
        {
            var options = new FindOneAndUpdateOptions<TDocument, TDocument>
            {
                ReturnDocument = ReturnDocument.After
            };

            return collection.FindOneAndUpdate(filter, update, options: options);
        }
    }
}
