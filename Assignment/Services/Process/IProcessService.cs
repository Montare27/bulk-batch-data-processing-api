namespace Assignment.Services.Process
{
    using Models;
    using Results;
    using System.Threading.Tasks;


    public interface IProcessService
    {
        Task<ProcessingResult> Process(Job job);
    }
}
