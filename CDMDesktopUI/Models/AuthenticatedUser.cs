using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDMDesktopUI.Models
{
    public class AuthenticatedUser
    {
        public string Access_token { get; set; }
        public string Token_type { get; set; }
        public int Expires_in { get; set; }
        public string userName { get; set; }
        //[JsonProperty(".issued")]
        //public string Issued { get; set; }
        //[JsonProperty(".expires")]
        //public string Expires { get; set; }
    }
}
