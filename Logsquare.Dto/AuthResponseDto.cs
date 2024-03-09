using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logsquare.Dto
{
    public class AuthResponseDto
    {
        [Required]
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
