namespace Assignment.Tests.ServiceTests
{
    using Common;
    using Models;
    using Services.Results;
    using System.Linq;
    using Xunit;

    public class BatchProcessTestClass : TestBase
    {
        [Fact]
        public async void BatchProcessMustReturnProcessSuccessful()
        {
            //Arrange
            var job = new Job
            {
                Type = new JobType{ Name = "batch" },
                Items = TestFactory.GetJobItemsWithCorrectAccountDataList(),
            };
            
            //Act
            var result = await JobService.Procedure(job);

            //Assert
            Assert.Equal(ProcessingValues.Success, result.Status);
        }

        [Fact]
        public async void BatchProcessMustReturnValidationFailed()
        {
            //Arrange
            var job = new Job
            {
                Type = new JobType{ Name = "batch"},
            };
            
            //Act
            var result = await JobService.Procedure(job);

            //Assert
            Assert.Equal(ProcessingValues.ValidationFailed, result.Status);
        }
        
        [Fact]
        public async void BatchProcessMustReturnUnSupportedFailed()
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
        public async void BatchProcessMustReturnProcessFailedDueAccountHasALittleMoney()
        {
            //Arrange
            var job = new Job
            {
                Type = new JobType{ Name = "batch" },
                Items = TestFactory.GetJobItemsWithInvalidAccountDataList(),
            };
            
            //Act
            var result = await JobService.Procedure(job);

            //Assert
            Assert.Equal(ProcessingValues.NotEnoughMoney, result.Status);
        }
        
        [Fact]
        public async void BatchProcessingDoNotAddNewJobTypesInDb()
        {
            //Arrange
            var batchJob = new Job
            {
                Type = new JobType{ Name = "batch" },
                Items = TestFactory.GetJobItemsWithInvalidAccountDataList(),
            };
            
            var countBefore = Context.JobTypes
                .ToList()
                .Count;
            
            //Act
            await JobService.Procedure(batchJob);
            
            var countAfter = Context.JobTypes
                .ToList()
                .Count;

            //Assert
            Assert.Equal(countBefore, countAfter);
        }
    }
}
