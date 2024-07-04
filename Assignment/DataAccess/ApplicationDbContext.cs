namespace Assignment.DataAccess
{
    using Models;
    using System;
    using System.Data.Common;
    using System.Data.Entity;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
            Database.CreateIfNotExists();
        }

        public ApplicationDbContext(DbConnection connection) : base(connection, true)
        {
            Database.SetInitializer<ApplicationDbContext>(null);
            Database.CreateIfNotExists();
        }
        
        public DbSet<Job> Jobs { get; set; }
        
        public DbSet<JobType> JobTypes { get; set; }
        
        public DbSet<JobItem> JobItems { get; set; }
        
        public DbSet<Account> Accounts { get; set; }
        
        public DbSet<LogEntry> LogEntries { get; set; }
        
        public DbSet<LogCollection> LogCollections { get; set; }
    }
}
