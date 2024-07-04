namespace Assignment.Tests.LogTests
{
    using Common;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using Xunit;

    public class JobLogTestClass : TestBase
    {
        [Fact]
        public async void LogsMustBeWrittenCorrectly()
        {
            //Arrange
            Guid jobId = Guid.NewGuid();

            var itemIds = Enumerable.Range(0, 5)
                .Select(_ => Guid.NewGuid()).ToList();

            var randomId1 = Guid.NewGuid();
            var randomId2 = Guid.NewGuid();
            
            var jobItems = new List<JobItem>()
            {
                new JobItem {AccountId = TestFactory.Id1, DestinationAccountId = TestFactory.Id2, Amount = 300, Id = itemIds[0]},
                new JobItem {AccountId = TestFactory.Id2, DestinationAccountId = TestFactory.Id1, Amount = 20, Id = itemIds[1]},
                new JobItem {AccountId = TestFactory.Id3, DestinationAccountId = TestFactory.Id4, Amount = 50, Id = itemIds[2]},
                new JobItem {AccountId = randomId1, DestinationAccountId = TestFactory.Id1, Amount = 200, Id = itemIds[3]},
                new JobItem {AccountId = TestFactory.Id5, DestinationAccountId = randomId2, Amount = 200, Id = itemIds[4]},
            };
            
            var job = new Job
            {
                Id = jobId,
                Type = new JobType{ Name = "bulk" },
                Items = jobItems,
            };
            
            var logs = new List<LogEntry>
            {
                new LogEntry() {ItemId = itemIds[0], Success = false, Description = $"Account by id {TestFactory.Id1} has not enough money in job {jobId}"},
                new LogEntry() {ItemId = itemIds[1], Success = true, Description = ""},
                new LogEntry() {ItemId = itemIds[2], Success = true, Description = ""},
                new LogEntry() {ItemId = itemIds[3], Success = false, Description = $"Account by id {randomId1} does not exist in job {jobId}"},
                new LogEntry() {ItemId = itemIds[4], Success = false, Description = $"Account by id {randomId2} does not exist in job {jobId}"},
            };
            
            //Act
            await JobService.Procedure(job);
            
            var logCollection = Context.LogCollections
                .FirstOrDefault(x => x.JobId == jobId);
            
            //Assert
            Assert.NotNull(logCollection);
            
            Assert.Equal(logs.Count, logCollection.Logs.Count);

            int count = 0;
            foreach (var log in logCollection.Logs)
            {
                Assert.Equal(logs[count].ItemId, log.ItemId);
                Assert.Equal(logs[count].Success, log.Success);
                Assert.Equal(logs[count].Description, log.Description);
                count++;
            }
        }
        
        [Fact]
        public async void LogsMustNotCoverEveryItemIfBatchProcessingFailed()
        {
            //Arrange
            Guid jobId = Guid.NewGuid();

            var itemIdColl = Enumerable.Range(0, 5)
                .Select(_ => Guid.NewGuid()).ToList();

            var randomAccId1 = Guid.NewGuid();
            var randomAccId2 = Guid.NewGuid();
            
            var jobItems = new List<JobItem>()
            {
                new JobItem {AccountId = TestFactory.Id2, DestinationAccountId = TestFactory.Id1, Amount = 20, Id = itemIdColl[0]},
                new JobItem {AccountId = TestFactory.Id1, DestinationAccountId = TestFactory.Id2, Amount = 300, Id = itemIdColl[1]},
                new JobItem {AccountId = TestFactory.Id3, DestinationAccountId = TestFactory.Id4, Amount = 50, Id = itemIdColl[2]},
                new JobItem {AccountId = randomAccId1, DestinationAccountId = TestFactory.Id1, Amount = 200, Id = itemIdColl[3]},
                new JobItem {AccountId = TestFactory.Id5, DestinationAccountId = randomAccId2, Amount = 200, Id = itemIdColl[4]},
            };
            
            var job = new Job
            {
                Id = jobId,
                Type = new JobType{ Name = "batch" },
                Items = jobItems,
            };
            
            var logs = new List<LogEntry>
            {
                new LogEntry() {ItemId = itemIdColl[0], Success = true, Description = $""},
                new LogEntry() {ItemId = itemIdColl[1], Success = false, Description = $"Account by id {TestFactory.Id1} has not enough money in job {jobId}"},
            };
            
            //Act
            await JobService.Procedure(job);
            
            var logCollection = await Context.LogCollections
                .FirstOrDefaultAsync(x => x.JobId.Equals(jobId));
            
            //Assert
            Assert.NotNull(logCollection);
            
            Assert.Equal(logs.Count, logCollection.Logs.Count);

            int count = 0;
            foreach (var log in logCollection.Logs)
            {
                Assert.Equal(logs[count].ItemId, log.ItemId);
                Assert.Equal(logs[count].Success, log.Success);
                Assert.Equal(logs[count].Description, log.Description);
                count++;
            }
        }
    }
}
