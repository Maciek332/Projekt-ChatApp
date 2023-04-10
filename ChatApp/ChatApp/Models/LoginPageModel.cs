using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Models
{
    public class LoginPageModel
    {
        public string LoginEmail { get; set; }
        public string LoginPassword { get; set; }

        public string RegisterEmail { get; set; }
        public string RegisterUserName { get; set; }
        public string RegisterPassword { get; set; }
        public string RegisterPasswordRepeat { get; set; }
    }
}
