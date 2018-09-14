using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.ZipKinService.AddressService
{
    public class AddressService : IAddressService
    {
        public string Test()
        {
            return "Date:" + DateTime.Now.ToString();
        }
    }
}
