namespace Assignment.DataAccess
{
    using Models;
    using System.Data.Entity;
    
    public class JobDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override async void Seed(ApplicationDbContext context)
        {
            var bulkType = new JobType(){Name = "bulk"};
            var batchType = new JobType(){Name = "batch"};
            
            context.JobTypes.Add(bulkType);
            context.JobTypes.Add(batchType);

            await context.SaveChangesAsync();
            
            base.Seed(context);
        }
    }
}
