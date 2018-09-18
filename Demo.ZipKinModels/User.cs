using FanQuick.Repository;
using System;

namespace Demo.ZipKinModels
{
    /// <summary>
    /// 用户。
    /// </summary>
    public class User : EntityBase
    {
        public string RealName { get; set; }
        public string Password { get; set; }

        public string Mobile { get; set; }
        public string Email { get; set; }
    }
}
