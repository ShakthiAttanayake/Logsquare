using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logsquare.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }
        public string Password { get; set; } = null!;
    }
}
