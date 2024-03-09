using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logsquare.Domain
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string? Email { get; set; }
        public string Password { get; set; }
        public bool IsDeleted { get; set; } = false;
        public byte[] Salt { get; set; }
    }
}
