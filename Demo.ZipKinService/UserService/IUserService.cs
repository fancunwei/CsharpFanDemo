using Demo.ZipKinModels;
using FanQuick.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.ZipKinService.UserService
{
    public interface IUserService 
    {
        User Get(string Id);
        void AddUser(User user);
        User UpdateRealName(User user);
        bool DeleteUser(string Id);
        Page<User> GetList();
    }
}
