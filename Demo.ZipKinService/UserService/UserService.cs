using Demo.ZipKinModels;
using FanQuick.Repository;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Demo.ZipKinService.UserService
{
    /// <summary>
    /// 
    /// </summary>
    public class UserService : IUserService
    {
        private IRepository<User> _repository;
        public UserService(IRepository<User> repository)
        {
            _repository = repository;
        }
        public User Get(string Id)
        {
          var res=  _repository.Find(m=>m.Id==Id).FirstOrDefault();
            return res;
        }
        /// <summary>
        /// 添加用户。
        /// </summary>
        /// <param name="user"></param>
        public void AddUser(User user)
        {
            _repository.Insert(user);
        }
        /// <summary>
        /// 删除用户。
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool DeleteUser(string Id)
        {
            var r = _repository.Delete(m => m.Id == Id);
            return r;
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public Page<User> GetList()
        {
            var query = _repository.Queryable;

            var res = Page<User>.GetPage(query, 1, 100);

            return res;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User UpdateRealName(User user)
        {
            var ups = new Dictionary<Expression<Func<User, object>>, object>
                    {
                        {m => m.RealName, user.RealName }
                    };

            var query = Builders<User>.Filter.Eq(t => t.Id, user.Id);
            var update = Builders<User>.Update.Set(m => m.RealName, user.RealName);

            var result = _repository.FindOneAndUpdate(query, update);
            return result;
        }
    }
}
