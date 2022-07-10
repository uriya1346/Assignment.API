using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assignments.Data
{
    public class DataContext : DbContext 
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options) { }

        public DbSet<AppAssignment> Assignments { get; set; }
        public DbSet<TaskArchive> TaskArchives { get; set; }
        public DbSet<TaskStatusOption> TaskStatuses { get; set; }
        public DbSet<TaskType> TaskTypes { get; set; }
    }
}
