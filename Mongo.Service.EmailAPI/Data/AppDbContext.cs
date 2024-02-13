using Microsoft.EntityFrameworkCore;
using Mongo.Service.EmailAPI.Models;

namespace Mongo.Service.EmailAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<EmailLogger> EmailLoggers { get; set; }

       
    }
}
