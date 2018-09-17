using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.ZipKinService.AddressService
{
    public class AddressService : IAddressService
    {
        public string Test()
        {
            Thread.Sleep(3000);
            return "Date:" + DateTime.Now.ToString();
        }
    }
}
