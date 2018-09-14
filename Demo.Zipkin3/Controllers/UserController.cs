using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.ZipKinModels;
using Demo.ZipKinService.UserService;
using FanQuick.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.ZipKin3.Controllers
{
    public class UserController : Controller
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public string Index() {
            return DateTime.Now.ToString();
        }
        [HttpGet]
        public User Get(string id)
        {
            var r= _userService.Get(id);
            return r == null ? new User() : r;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public bool Add(User user)
        {
            _userService.AddUser(user);
            return true;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
       public  User UpdateRealName(User user)
        {
            return _userService.UpdateRealName(user);
        }
        /// <summary>
        /// 删除用户。
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
       public bool DeleteUser(string Id) {
            return _userService.DeleteUser(Id);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
       public Page<User> GetList() {
            return _userService.GetList();
        }
    }
}