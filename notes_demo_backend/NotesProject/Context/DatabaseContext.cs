using Microsoft.EntityFrameworkCore;

using NotesProject.Models;

namespace NotesProject.Context
{
    public class DatabaseContext : DbContext 
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<NotesModel> Notes { get; set; }
    }
}
