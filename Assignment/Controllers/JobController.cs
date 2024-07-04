namespace Assignment.Controllers
{
    using DataAccess.Interfaces;
    using Dtos;
    using Mapper;
    using Services.Logging;
    using Services.Process;
    using Services.Results;
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;

    [RoutePrefix("api/job")]
    public class JobController : ApiController
    {
        private readonly IJobLogger _jobLogger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly JobServiceStrategy _jobServiceStrategy;

        public JobController(IUnitOfWork unitOfWork, JobServiceStrategy jobServiceStrategy, IJobLogger jobLogger)
        {
            _unitOfWork = unitOfWork;
            _jobServiceStrategy = jobServiceStrategy;
            _jobLogger = jobLogger;
        }
        
        /// <summary>
        /// Starts new job
        /// </summary>
        /// <param name="jobDto"></param>
        /// <returns>
        /// Status 200 if Job was processed successfully
        /// Status 400 if ModelState is not valid
        /// Status 400 if Job's process finished with errors
        /// Also instance of JobResponse with values about job id and description of error if exist 
        /// </returns>
        //POST api/job/startJob
        [HttpPost]
        [Route("startJob")]
        public async Task<HttpResponseMessage> StartJob([FromBody] JobDto jobDto)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            var job = Mapper.MapJobDtoToJob(jobDto);
            
            var result = await _jobServiceStrategy.Procedure(job);

            var response = new JobResponse
            {
                Id = result.JobId, 
                ErrorMessage = result.Message,
            };
            
            return result.Status == ProcessingValues.Success
                ? Request.CreateResponse(HttpStatusCode.OK, response)
                : Request.CreateResponse(HttpStatusCode.BadRequest, response);
        }
        
        /// <summary>
        /// Get logs by id of job
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Status 400 if job by id was not found
        /// Status 200 otherwise
        /// Also list of logs
        /// </returns>
        //GET api/job/jobLogs
        [HttpGet]
        [Route("jobLogs")]
        public async Task<HttpResponseMessage> GetJobLogs(Guid id)
        {
            var job = await _unitOfWork.JobRepository.GetById(id);
            
            if (job == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, $"Job with id {id} was not found");
            }

            var enumerable = await _jobLogger.GetLogs(id);

            var logs = enumerable.Select(x => new{x.ItemId, x.Success, x.Description});

            return Request.CreateResponse(HttpStatusCode.OK, logs);
        }
    }
}
