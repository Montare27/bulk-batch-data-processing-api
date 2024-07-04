namespace Assignment.Tests.ServiceTests
{
    using Common;
    using Models;
    using Services.Results;
    using System.Linq;
    using Xunit;

    public class BulkProcessTestClass : TestBase
    {
        [Fact]
        public async void BulkProcessMustReturnProcessSuccessful()
        {
            //Arrange
            var job = new Job
            {
                Type = new JobType{ Name = "bulk" },
                Items = TestFactory.GetJobItemsWithCorrectAccountDataList(),
            };
            
            //Act
            var result = await JobService.Procedure(job);

            //Assert
            Assert.Equal(ProcessingValues.Success, result.Status);
        }

        [Fact]
        public async void BulkProcessMustReturnValidationFailed()
        {
            //Arrange
            var job = new Job
            {
                Type = new JobType{ Name = "bulk"}
            };
            
            //Act
            var result = await JobService.Procedure(job);

            //Assert
            Assert.Equal(ProcessingValues.ValidationFailed, result.Status);
        }
        
        [Fact]
        public async void BulkProcessMustReturnUnSupportedFailed()
        {
            //Arrange
            var job = new Job
            {
                Type = new JobType{ Name = "unknown" },
                Items = TestFactory.GetJobItemsWithCorrectAccountDataList(),
            };
            
            //Act
            var result = await JobService.Procedure(job);

            //Assert
            Assert.Equal(ProcessingValues.UnSupportedType, result.Status);
        }
        
        [Fact]
        public async void BulkProcessMustReturnProcessFailedDueAccountHasALittleMoney()
        {
            //Arrange
            var job = new Job
            {
                Type = new JobType{ Name = "bulk" },
                Items = TestFactory.GetJobItemsWithInvalidAccountDataList(),
            };
            
            //Act
            var result = await JobService.Procedure(job);

            //Assert
            Assert.Equal(ProcessingValues.NotEnoughMoney, result.Status);
        }
        
        [Fact]
        public async void BulkProcessingDoNotAddNewJobTypesInDb()
        {
            //Arrange
            var bulkJob = new Job
            {
                Type = new JobType{ Name = "bulk" },
                Items = TestFactory.GetJobItemsWithInvalidAccountDataList(),
            };
            
            var countBefore = Context.JobTypes
                .ToList()
                .Count;
            
            //Act
            await JobService.Procedure(bulkJob);
            
            var countAfter = Context.JobTypes
                .ToList()
                .Count;

            //Assert
            Assert.Equal(countBefore, countAfter);
        }
    }
}
