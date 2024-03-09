using Logsquare.AthenticationProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logsquare.AthenticationProvider
{
    public interface IAuthenticationService
    {
        Task<AuthResult> GenerateJWTTokenAsync(IdentityUser identityUser);
    }
}
