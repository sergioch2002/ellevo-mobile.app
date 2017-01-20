using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ellevo.mobile.app.objects
{
    public class Token
    {
        public string Access_token { get; set; }
        public string Expires_in { get; set; }
        public string Token_type { get; set; }
    }
}
