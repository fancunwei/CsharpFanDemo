using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.ZipKinModels;
using Demo.ZipKinService.UserService;
using FanQuick.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Demo.ZipKin.Controllers
{
    public class UserController : Controller
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public string Index()
        {
            return DateTime.Now.ToString();
        }
        [HttpGet]
        public User Get(string id)
        {
            var r = _userService.Get(id);
            return r == null ? new User() : r;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add([FromBody]User user)
        {
            _userService.AddUser(user);
            return Content(JsonConvert.SerializeObject(user));
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
         [HttpPost]
        public User UpdateRealName([FromBody]User user)
        {
            return _userService.UpdateRealName(user);
        }
        /// <summary>
        /// 删除用户。
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public bool DeleteUser(string Id)
        {
            return _userService.DeleteUser(Id);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Page<User> GetList()
        {
            return _userService.GetList();
        }
    }
}