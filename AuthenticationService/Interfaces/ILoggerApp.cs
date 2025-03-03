namespace AuthenticationService.Interfaces
{
    public interface ILoggerApp
    {
        void WriteEvent(string eventMessage);
        void WriteError(string errorMessage);
    }
}
