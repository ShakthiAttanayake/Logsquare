using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logsquare.AthenticationProvider.Models
{
    public class AuthResult
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
