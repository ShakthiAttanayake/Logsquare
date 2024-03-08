using Logsquare.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logsquare.Infrastructure.Persistance
{
    public class LogsqureDbContext : DbContext
    {
        public LogsqureDbContext(DbContextOptions<LogsqureDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }

    }
}
