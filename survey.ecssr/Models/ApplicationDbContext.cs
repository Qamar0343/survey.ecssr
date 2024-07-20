using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace survey.ecssr.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Survey> Survey { get; set; } = null!;
        public DbSet<Question> Question { get; set; } = null!;
        public DbSet<Options> Options { get; set; } = null!;
        public DbSet<ControlType> ControlType { get; set; } = null!;
        public DbSet<Answer> Answer { get; set; } = null!;
        public DbSet<Response> Response { get; set; } = null!;
    }
}
