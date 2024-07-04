namespace Assignment.Services.ProcessDictionary
{
    using Process;

    public interface IProcessServicesDictionary
    {
        IProcessService TryGetValue(string name);
    }
}
