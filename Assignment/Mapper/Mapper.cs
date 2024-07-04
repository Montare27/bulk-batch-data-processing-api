namespace Assignment.Mapper
{
    using Dtos;
    using Models;
    using System.Linq;

    /// <summary>
    /// Class with logic about mapping on entity to another
    /// </summary>
    public static class Mapper
    {
        /// <summary>
        /// Maps JobDto to Job
        /// </summary>
        /// <param name="jobDto"></param>
        /// <returns>
        /// new instance of Job with data from jobDto
        /// </returns>
        public static Job MapJobDtoToJob(JobDto jobDto)
        {
            var jobItems = jobDto.Items.Select(itemDto => new JobItem
            {
                Amount = itemDto.Amount,
                DestinationAccountId = itemDto.DestinationAccountId,
                AccountId = itemDto.AccountId,
            }).ToList();
            
            return new Job
            {
                Type = new JobType
                {
                    Name = jobDto.JobType,
                },
                
                Items = jobItems,
            };
        }
    }
}
