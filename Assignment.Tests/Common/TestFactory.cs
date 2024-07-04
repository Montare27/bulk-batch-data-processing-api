namespace Assignment.Tests.Common
{
    using DataAccess;
    using Effort;
    using Models;
    using System;
    using System.Collections.Generic;

    public static class TestFactory
    {
        public static ApplicationDbContext CreateContext()
        {
            var connection = DbConnectionFactory.CreateTransient();
            
            var context = new ApplicationDbContext(connection);
            
            context.Database.CreateIfNotExists();
            
            context.JobTypes.AddRange(GetTypeList());
            context.Accounts.AddRange(GetAccountList());
            
            context.SaveChanges();

            return context;
        }

        public static void Destroy(ApplicationDbContext context)
        {
            context.Database.Delete();
            context.Dispose();
        }
        
        public readonly static Guid Id1 = Guid.NewGuid();
        public readonly static Guid Id2 = Guid.NewGuid();
        public readonly static Guid Id3 = Guid.NewGuid();
        public readonly static Guid Id4 = Guid.NewGuid();
        public readonly static Guid Id5 = Guid.NewGuid();
        
        public static List<JobType> GetTypeList()
        {
            return new List<JobType>
            {
                new JobType{Name = "batch"},
                new JobType{Name = "bulk"},
            };
        }
        
        public static List<Account> GetAccountList()
        {
            return new List<Account>
            {
                new Account()
                {
                    Id = Id1,
                    Username = "Bill",
                    Amount = 100,
                    Password = "xxxxxxxxx",
                },
                new Account()
                {
                    Id = Id2,
                    Username = "Tomas",
                    Amount = 200,
                    Password = "xxxxxxxxx",
                },
                new Account()
                {
                    Id = Id3,
                    Username = "Ryan",
                    Amount = 300,
                    Password = "xxxxxxxxx",
                },
                new Account()
                {
                    Id = Id4,
                    Username = "Jill",
                    Amount = 400,
                    Password = "xxxxxxxxx",
                },
                new Account()
                {
                    Id = Id5,
                    Username = "Alfred",
                    Amount = 500,
                    Password = "xxxxxxxxx",
                },
            };
        }
        
        public static List<JobItem> GetJobItemsWithCorrectAccountDataList()
        {
            return new List<JobItem>()
            {
                new JobItem()
                {
                    AccountId = Id1,
                    DestinationAccountId = Id2,
                    Amount = 100,
                },
                new JobItem()
                {
                    AccountId = Id2,
                    DestinationAccountId = Id1,
                    Amount = 200,
                },
                new JobItem()
                {
                    AccountId = Id3,
                    DestinationAccountId = Id4,
                    Amount = 50,
                },
                new JobItem()
                {
                    AccountId = Id1,
                    DestinationAccountId = Id5,
                    Amount = 25,
                },
                new JobItem()
                {
                    AccountId = Id3,
                    DestinationAccountId = Id4,
                    Amount = 200,
                },
            };
        }
        
        public static List<JobItem> GetJobItemsWithInvalidAccountDataList()
        {
            return new List<JobItem>()
            {
                new JobItem()
                {
                    AccountId = Id1,
                    DestinationAccountId = Id2,
                    Amount = 200,
                },
                new JobItem()
                {
                    AccountId = Id2,
                    DestinationAccountId = Id1,
                    Amount = 500,
                },
                new JobItem()
                {
                    AccountId = Id3,
                    DestinationAccountId = Id4,
                    Amount = 200,
                },
                new JobItem()
                {
                    AccountId = Id1,
                    DestinationAccountId = Id5,
                    Amount = 100,
                },
                new JobItem()
                {
                    AccountId = Id3,
                    DestinationAccountId = Id4,
                    Amount = 1,
                },
            };
        }
    }
}
