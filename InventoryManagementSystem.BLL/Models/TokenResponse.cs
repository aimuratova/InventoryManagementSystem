using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Models
{
    public class TokenResponse
    {
        public string access_token { get; set; }
        public string instance_url { get; set; }

        public string AccessToken => access_token;
        public string InstanceUrl => instance_url;
    }
}
