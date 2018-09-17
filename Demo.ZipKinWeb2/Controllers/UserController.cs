using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.ZipkinCommon;
using Demo.ZipKinModels;
using Demo.ZipKinService.AddressService;
using Demo.ZipKinService.UserService;
using FanQuick.Config;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace Demo.ZipKinWeb2.Controllers
{
    public class UserController : Controller
    {
        private IUserService _userService;
        private IAddressService _addressService;
        public UserController(IUserService userService, IAddressService addressService)
        {
            _userService = userService;
            _addressService = addressService;
        }
        [HttpGet]
        public string Index()
        {
            return DateTime.Now.ToString() + _addressService.Test();
        }
        [HttpGet]
        public IActionResult Get(string id)
        {
            var r = _userService.Get(id);
            var n = r == null ? new User() : r;

            return Content(JsonConvert.SerializeObject(n));
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add([FromBody]User user)
        {
            _userService.AddUser(user);


            //模拟调用其他站点请求。
            var url = $"{ConfigEx.WebSite}/user/get?id={user.Id}";
            var content = HTTPHelper.GetAsync(url, null);
            return Content(content.Result);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UpdateRealName([FromBody]User user)
        {
            var res = _userService.UpdateRealName(user);

            return Content(JsonConvert.SerializeObject(res));
        }
        /// <summary>
        /// 删除用户。
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteUser(string Id)
        {
            var res = _userService.DeleteUser(Id);
            return Json(new { data = res });
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetList()
        {
            var res = _userService.GetList();
            return Json(new { data = res });
        }
    }
}