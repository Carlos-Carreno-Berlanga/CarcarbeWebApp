namespace ConsoleDaemonProducer.Services.Interfaces
{
    public interface IEnviromentVariableService
    {
        void CreateIfNotExists(string enviromentVariableName, string enviromentVariableValue);
    }
}
